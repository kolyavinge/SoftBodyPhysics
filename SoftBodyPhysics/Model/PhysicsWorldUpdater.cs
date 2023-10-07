using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Intersections;

namespace SoftBodyPhysics.Model;

internal interface IPhysicsWorldUpdater
{
    void Update();
}

internal class PhysicsWorldUpdater : IPhysicsWorldUpdater
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly ISegmentIntersectDetector _segmentIntersectDetector;
    private readonly ISoftBodyBordersUpdater _softBodyBordersUpdater;
    private readonly INormalCalculator _normalCalculator;
    private readonly IPhysicsUnits _physicsUnits;

    public PhysicsWorldUpdater(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        INormalCalculator normalCalculator,
        ISegmentIntersectDetector segmentIntersectDetector,
        ISoftBodyBordersUpdater softBodyBordersUpdater,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _segmentIntersectDetector = segmentIntersectDetector;
        _softBodyBordersUpdater = softBodyBordersUpdater;
        _normalCalculator = normalCalculator;
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
        foreach (var item in _softBodiesCollection.MassPointsToCheckCollisions)
        {
            var massPoint = item.MassPoint;
            var body = item.Body;

            if (!(body.Borders.MinX - 10.0 <= massPoint.Position.X && massPoint.Position.X <= body.Borders.MaxX + 10.0 &&
                  body.Borders.MinY - 10.0 <= massPoint.Position.Y && massPoint.Position.Y <= body.Borders.MaxY + 10.0)) continue;

            foreach (var edge in body.SpringsToCheckCollisions)
            {
                if (!_segmentIntersectDetector.Check(edge.PointA.Position, edge.PointB.Position, massPoint.Position)) continue;
                var normal = _normalCalculator.GetNormal(edge.PointA.Position, edge.PointB.Position);

                edge.PointA.Position = edge.PointA.PrevPosition;
                edge.PointB.Position = edge.PointB.PrevPosition;

                edge.PointA.Velocity -= 2.0 * (edge.PointA.Velocity * normal) * normal;
                edge.PointB.Velocity -= 2.0 * (edge.PointB.Velocity * normal) * normal;
                edge.PointA.Velocity *= 1.0 - _physicsUnits.Friction;
                edge.PointB.Velocity *= 1.0 - _physicsUnits.Friction;

                massPoint.State = CollisionState.Collision;
                massPoint.Position = massPoint.PrevPosition;
                massPoint.Velocity -= 2.0 * (massPoint.Velocity * normal) * normal;
                massPoint.Velocity *= 1.0 - _physicsUnits.Friction;

                break;
            }
        }
    }

    private void CheckHardBodyCollisions()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            foreach (var body in _hardBodiesCollection.HardBodies)
            {
                if (!(body.Borders.MinX - 1.0 <= massPoint.Position.X && massPoint.Position.X <= body.Borders.MaxX + 1.0 &&
                      body.Borders.MinY - 1.0 <= massPoint.Position.Y && massPoint.Position.Y <= body.Borders.MaxY + 1.0)) continue;

                foreach (var edge in body.Edges)
                {
                    if (!_segmentIntersectDetector.Check(edge.From, edge.To, massPoint.Position)) continue;

                    var normal = _normalCalculator.GetNormal(edge.From, edge.To);
                    edge.State = CollisionState.Collision;
                    massPoint.State = CollisionState.Collision;
                    massPoint.Position = massPoint.PrevPosition;
                    massPoint.Velocity -= 2.0 * (massPoint.Velocity * normal) * normal;
                    massPoint.Velocity *= 1.0 - _physicsUnits.Friction;

                    break;
                }
            }
        }
    }
}
