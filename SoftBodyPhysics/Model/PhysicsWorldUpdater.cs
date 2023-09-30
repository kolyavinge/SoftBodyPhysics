using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Intersections;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

internal interface IPhysicsWorldUpdater
{
    void Update();
}

internal class PhysicsWorldUpdater : IPhysicsWorldUpdater
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly ISegmentIntersector _segmentIntersector;
    private readonly INormalCalculator _normalCalculator;
    private readonly ISoftBodyIntersector _softBodyIntersector;
    private readonly IPhysicsUnits _physicsUnits;

    public PhysicsWorldUpdater(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        INormalCalculator normalCalculator,
        ISoftBodyIntersector softBodyIntersector,
        ISegmentIntersector segmentIntersector,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _segmentIntersector = segmentIntersector;
        _normalCalculator = normalCalculator;
        _softBodyIntersector = softBodyIntersector;
        _physicsUnits = physicsUnits;
    }

    public void Update()
    {
        InitMassPoints();
        ApplyGravityForce();
        ApplySpringForce();
        ApplyVelocity();
        ApplyPositions();
        ApplySoftBodyCollisions();
        ApplyHardBodyCollisions();
    }

    private void InitMassPoints()
    {
        _softBodiesCollection.AllMassPoints.Each(x => x.ResetForce());
        _softBodiesCollection.AllMassPoints.Each(x => x.ResetState());
    }

    private void ApplyGravityForce()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Force += new Vector(0.0, -_physicsUnits.GravityAcceleration * massPoint.Mass);
        }
    }

    private void ApplySpringForce()
    {
        foreach (var spring in _softBodiesCollection.AllSprings)
        {
            var fs = spring.Stiffness * spring.DeformLength;
            var fd = _physicsUnits.SpringDamper * ((spring.PointB.Position - spring.PointA.Position).Normalized * (spring.PointB.Velocity - spring.PointA.Velocity));
            var f = fs + fd;
            spring.PointA.Force += f * (spring.PointB.Position - spring.PointA.Position).Normalized;
            spring.PointB.Force += f * (spring.PointA.Position - spring.PointB.Position).Normalized;
        }
    }

    private void ApplyVelocity()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Velocity += massPoint.Force * (_physicsUnits.TimeDelta / massPoint.Mass);
        }
    }

    private void ApplyPositions()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.SavePosition();
            massPoint.Position += massPoint.Velocity * _physicsUnits.TimeDelta;
        }
    }

    private void ApplySoftBodyCollisions()
    {
        foreach (var (body1, body2) in _softBodiesCollection.SoftBodies.GetCrossProduct())
        {
            foreach (var massPoint in body2.MassPoints)
            {
                var intersectPoint = _softBodyIntersector.GetIntersectPoint(body1, massPoint.Position);
                if (intersectPoint is null) continue;
                var normal = _normalCalculator.GetNormal(intersectPoint.Spring.PointA.Position, intersectPoint.Spring.PointB.Position);
                massPoint.State = MassPointState.Collision;
                massPoint.Position = intersectPoint.Point;
                massPoint.Velocity -= 2.0 * (massPoint.Velocity * normal) * normal;
                massPoint.Velocity *= 1.0 - _physicsUnits.Friction;
                massPoint.Position += massPoint.Velocity.Normalized * 0.1;
            }
        }
    }

    private void ApplyHardBodyCollisions()
    {
        foreach (var edge in _hardBodiesCollection.AllEdges)
        {
            foreach (var massPoint in _softBodiesCollection.AllMassPoints)
            {
                var intersectPoint = _segmentIntersector.GetIntersectPoint(edge.From, edge.To, massPoint.PrevPosition, massPoint.Position);
                if (intersectPoint is null) continue;
                var normal = _normalCalculator.GetNormal(edge.From, edge.To);
                massPoint.State = MassPointState.Collision;
                massPoint.Position = intersectPoint.Value;
                massPoint.Velocity -= 2.0 * (massPoint.Velocity * normal) * normal;
                massPoint.Velocity *= 1.0 - _physicsUnits.Friction;
                massPoint.Position += massPoint.Velocity.Normalized * 0.1;
            }
        }
    }
}
