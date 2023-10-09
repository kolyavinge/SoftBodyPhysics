using System;
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
        foreach (var softBody in _softBodiesCollection.SoftBodies)
        {
            foreach (var massPoint in softBody.MassPoints)
            {
                massPoint.Position += massPoint.Velocity * Constants.TimeStep;
                if ((massPoint.Position - massPoint.PrevPosition).LengthSquare > 0.00001)
                {
                    CheckSoftBodyCollisions(softBody);
                    CheckHardBodyCollisions(softBody);
                    massPoint.SavePosition();
                }
            }
        }
    }

    private void CheckSoftBodyCollisions(SoftBody body1)
    {
        foreach (var body2 in _softBodiesCollection.SoftBodies)
        {
            if (body2 == body1) continue;

            if (Math.Abs(body1.Borders.MiddleX - body2.Borders.MiddleX) > body1.Borders.Width + body2.Borders.Width) continue;
            if (Math.Abs(body1.Borders.MiddleY - body2.Borders.MiddleY) > body1.Borders.Height + body2.Borders.Height) continue;

            foreach (var massPoint in body1.MassPoints)
            {
                if (!(body2.Borders.MinX - 10.0 < massPoint.Position.X && massPoint.Position.X < body2.Borders.MaxX + 10.0 &&
                      body2.Borders.MinY - 10.0 < massPoint.Position.Y && massPoint.Position.Y < body2.Borders.MaxY + 10.0)) continue;

                CheckMassPointAndSpringsCollision(massPoint, body2.SpringsToCheckCollisions);
            }

            foreach (var massPoint in body2.MassPoints)
            {
                if (!(body1.Borders.MinX - 10.0 < massPoint.Position.X && massPoint.Position.X < body1.Borders.MaxX + 10.0 &&
                      body1.Borders.MinY - 10.0 < massPoint.Position.Y && massPoint.Position.Y < body1.Borders.MaxY + 10.0)) continue;

                CheckMassPointAndSpringsCollision(massPoint, body1.SpringsToCheckCollisions);
            }
        }
    }

    private void CheckMassPointAndSpringsCollision(MassPoint massPoint, Spring[] springsToCheckCollisions)
    {
        foreach (var spring in springsToCheckCollisions)
        {
            if (!_segmentIntersectDetector.Intersected(spring.PointA.Position, spring.PointB.Position, massPoint.Position)) continue;
            var normal = _normalCalculator.GetNormal(spring.PointA.Position, spring.PointB.Position);

            spring.PointA.Position = spring.PointA.PrevPosition;
            spring.PointB.Position = spring.PointB.PrevPosition;

            spring.PointA.Velocity -= 2.0 * (spring.PointA.Velocity * normal) * normal; // reflected vectors
            spring.PointB.Velocity -= 2.0 * (spring.PointB.Velocity * normal) * normal;
            spring.PointA.Velocity *= 1.0 - _physicsUnits.Friction;
            spring.PointB.Velocity *= 1.0 - _physicsUnits.Friction;

            massPoint.State = CollisionState.Collision;
            massPoint.Position = massPoint.PrevPosition;
            massPoint.Velocity -= 2.0 * (massPoint.Velocity * normal) * normal; // reflected vector
            massPoint.Velocity *= 1.0 - _physicsUnits.Friction;

            return;
        }
    }

    private void CheckHardBodyCollisions(SoftBody softBody)
    {
        foreach (var massPoint in softBody.MassPoints)
        {
            foreach (var hardBody in _hardBodiesCollection.HardBodies)
            {
                if (!(hardBody.Borders.MinX - 1.0 < massPoint.Position.X && massPoint.Position.X < hardBody.Borders.MaxX + 1.0 &&
                      hardBody.Borders.MinY - 1.0 < massPoint.Position.Y && massPoint.Position.Y < hardBody.Borders.MaxY + 1.0)) continue;

                foreach (var edge in hardBody.Edges)
                {
                    if (!_segmentIntersectDetector.Intersected(edge.From, edge.To, massPoint.Position)) continue;

                    var normal = _normalCalculator.GetNormal(edge.From, edge.To);
                    edge.State = CollisionState.Collision;
                    massPoint.State = CollisionState.Collision;
                    massPoint.Position = massPoint.PrevPosition;
                    massPoint.Velocity -= 2.0 * (massPoint.Velocity * normal) * normal; // reflected vector
                    massPoint.Velocity *= 1.0 - _physicsUnits.Friction;

                    break;
                }
            }
        }
    }
}
