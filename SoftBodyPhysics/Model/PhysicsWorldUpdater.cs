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
    private readonly ISpringIntersector _springIntersector;
    private readonly IPhysicsUnits _physicsUnits;

    public PhysicsWorldUpdater(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        ISegmentIntersector segmentIntersector,
        INormalCalculator normalCalculator,
        ISpringIntersector springIntersector,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _segmentIntersector = segmentIntersector;
        _normalCalculator = normalCalculator;
        _springIntersector = springIntersector;
        _physicsUnits = physicsUnits;
    }

    public void Update()
    {
        InitMassPoints();
        ApplyGravityForce();
        ApplySpringForce();
        ApplyVelocity();
        ApplyPositions();
        ApplyHardBodyCollisions();
        ApplySoftBodyCollisions();
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

    private void ApplySoftBodyCollisions()
    {
        foreach (var (body1, body2) in _softBodiesCollection.SoftBodies.GetCrossProduct())
        {
            foreach (var spring in body1.Springs)
            {
                foreach (var massPoint in body2.MassPoints)
                {
                    var intersectPoint = _segmentIntersector.GetIntersectPoint(
                        spring.PointA.Position, spring.PointB.Position, massPoint.PrevPosition, massPoint.Position);

                    if (intersectPoint is not null)
                    {
                        //spring.PointA.Velocity += 0.1 * massPoint.Velocity;
                        //spring.PointB.Velocity += 0.1 * massPoint.Velocity;
                        var normal = _normalCalculator.GetNormal(spring.PointA.Position, spring.PointB.Position);
                        massPoint.State = MassPointState.Collision;
                        massPoint.Position = intersectPoint.Value;
                        massPoint.Velocity -= 2.0 * (massPoint.Velocity * normal) * normal;
                        massPoint.Velocity *= 1.0 - _physicsUnits.Friction;
                        massPoint.Position += massPoint.Velocity.Normalized * 0.1;
                    }

                    intersectPoint = _springIntersector.GetIntersectPoint(
                        spring.PointA.Position,
                        spring.PointB.Position,
                        spring.PointA.PrevPosition,
                        spring.PointB.PrevPosition,
                        massPoint.Position);

                    if (intersectPoint is not null)
                    {
                        massPoint.State = MassPointState.Collision;
                        massPoint.Position = intersectPoint.Value;
                        massPoint.Velocity += 0.5 * (spring.PointA.Velocity + spring.PointB.Velocity);
                        massPoint.Position += (spring.PointA.Velocity + spring.PointB.Velocity).Normalized * 0.1;
                    }
                }
            }
        }
    }
}
