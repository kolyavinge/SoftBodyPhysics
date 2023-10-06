using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Intersections;
using SoftBodyPhysics.Utils;
using System.Runtime.InteropServices;

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
    private readonly ISegmentIntersectDetector _segmentIntersectDetector;
    private readonly ISoftBodyBordersUpdater _softBodyBordersUpdater;
    private readonly INormalCalculator _normalCalculator;
    private readonly ISoftBodyIntersector _softBodyIntersector;
    private readonly IPhysicsUnits _physicsUnits;

    public PhysicsWorldUpdater(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        INormalCalculator normalCalculator,
        ISoftBodyIntersector softBodyIntersector,
        ISegmentIntersector segmentIntersector,
        ISegmentIntersectDetector segmentIntersectDetector,
        ISoftBodyBordersUpdater softBodyBordersUpdater,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _segmentIntersector = segmentIntersector;
        _segmentIntersectDetector = segmentIntersectDetector;
        _softBodyBordersUpdater = softBodyBordersUpdater;
        _normalCalculator = normalCalculator;
        _softBodyIntersector = softBodyIntersector;
        _physicsUnits = physicsUnits;
    }

    public void Update()
    {
        Init();
        for (var time = 0.0; time < _physicsUnits.Time; time += Constants.TimeStep)
        {
            InitGravityForce();
            ApplySpringForce();
            ApplyVelocity();
            ApplyPositions();
            _softBodyBordersUpdater.UpdateBorders(_softBodiesCollection.SoftBodies);
        }
    }

    private void Init()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.ResetState();
        }
        foreach (var spring in _hardBodiesCollection.AllEdges)
        {
            spring.ResetState();
        }
    }

    private void InitGravityForce()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Force = new Vector(0.0, -_physicsUnits.GravityAcceleration * massPoint.Mass);
        }
    }

    private void ApplySpringForce()
    {
        foreach (var spring in _softBodiesCollection.AllSprings)
        {
            var a = spring.PointA;
            var b = spring.PointB;
            var fs = spring.Stiffness * spring.DeformLength;
            var diffBA = (b.Position - a.Position).Normalized;
            var fd = _physicsUnits.SpringDamper * (diffBA * (b.Velocity - a.Velocity)); // (B.Position - A.Position).Normalized * (B.Velocity - A.Velocity)
            var f = fs + fd;
            a.Force += f * diffBA; // (B.Position - A.Position).Normalized
            b.Force -= f * diffBA; // (A.Position - B.Position).Normalized
        }
    }

    private void ApplyVelocity()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Velocity += massPoint.Force * (Constants.TimeStep / massPoint.Mass);
        }
    }

    private void ApplyPositions()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Position += massPoint.Velocity * Constants.TimeStep;
            CheckSoftBodyCollisions();
            CheckHardBodyCollisions();
            massPoint.SavePosition();
        }
    }

    private void CheckSoftBodyCollisions()
    {
        foreach (var (body1, body2) in _softBodiesCollection.SoftBodiesCrossProduct)
        {
            foreach (var massPoint in body1.MassPoints)
            {
                if (!body2.Borders.IsPointIn(massPoint.Position, 10.0)) continue;
                foreach (var edge in body2.Edges)
                {
                    var edgePointA = edge.PointA;
                    var edgePointB = edge.PointB;

                    if (!_segmentIntersectDetector.Check(edgePointA.Position, edgePointB.Position, massPoint.Position)) continue;
                    var normal = _normalCalculator.GetNormal(edgePointA.Position, edgePointB.Position);

                    edgePointA.Position = edgePointA.PrevPosition;
                    edgePointB.Position = edgePointB.PrevPosition;

                    edgePointA.Velocity -= 2.0 * (edgePointA.Velocity * normal) * normal;
                    edgePointB.Velocity -= 2.0 * (edgePointB.Velocity * normal) * normal;
                    edgePointA.Velocity *= 1.0 - _physicsUnits.Friction;
                    edgePointB.Velocity *= 1.0 - _physicsUnits.Friction;

                    massPoint.State = CollisionState.Collision;
                    massPoint.Position = massPoint.PrevPosition;
                    massPoint.Velocity -= 2.0 * (massPoint.Velocity * normal) * normal;
                    massPoint.Velocity *= 1.0 - _physicsUnits.Friction;

                    break;
                }
            }
        }
    }

    private void CheckHardBodyCollisions()
    {
        foreach (var edge in _hardBodiesCollection.AllEdges)
        {
            foreach (var massPoint in _softBodiesCollection.AllMassPoints)
            {
                if (!_segmentIntersectDetector.Check(edge.From, edge.To, massPoint.Position)) continue;
                var normal = _normalCalculator.GetNormal(edge.From, edge.To);
                edge.State = CollisionState.Collision;
                massPoint.State = CollisionState.Collision;
                massPoint.Position = massPoint.PrevPosition;
                massPoint.Velocity -= 2.0 * (massPoint.Velocity * normal) * normal;
                massPoint.Velocity *= 1.0 - _physicsUnits.Friction;
            }
        }
    }
}
