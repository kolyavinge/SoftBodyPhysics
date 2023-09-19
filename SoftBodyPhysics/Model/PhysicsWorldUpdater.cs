﻿using SoftBodyPhysics.Utils;

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
    private readonly INormalDetector _normalDetector;

    public PhysicsWorldUpdater(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        ISegmentIntersector segmentIntersector,
        INormalDetector normalDetector)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _segmentIntersector = segmentIntersector;
        _normalDetector = normalDetector;
    }

    public void Update()
    {
        _softBodiesCollection.AllMassPoints.Each(x => x.ResetForce());
        ApplyGravityForce();
        ApplySpringForce();
        ApplyVelocityAndPosition();
        ApplyHardBodyCollisions();
        ApplySoftBodyCollisions();
    }

    private void ApplyGravityForce()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Force += new Vector2d(0.0, -Constants.GravityAcceleration * massPoint.Mass);
        }
    }

    private void ApplySpringForce()
    {
        foreach (var spring in _softBodiesCollection.AllSprings)
        {
            var fs = spring.Stiffness * spring.DeformLength;
            var fd = Constants.SpringDamper * ((spring.PointB.Position - spring.PointA.Position).Normalized * (spring.PointB.Velocity - spring.PointA.Velocity));
            var f = fs + fd;
            spring.PointA.Force += f * (spring.PointB.Position - spring.PointA.Position).Normalized;
            spring.PointB.Force += f * (spring.PointA.Position - spring.PointB.Position).Normalized;
        }
    }

    private void ApplyVelocityAndPosition()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Velocity += massPoint.Force * (Constants.TimeDelta / massPoint.Mass);
            massPoint.SavePosition();
            massPoint.Position += massPoint.Velocity * Constants.TimeDelta;
        }
    }

    private void ApplyHardBodyCollisions()
    {
        foreach (var edge in _hardBodiesCollection.AllEdges)
        {
            foreach (var massPoint in _softBodiesCollection.AllMassPoints)
            {
                var intersectPoint = _segmentIntersector.GetIntersectPoint(edge.From, edge.To, massPoint.PrevPosition, massPoint.Position);
                if (intersectPoint == null) continue;
                var normal = _normalDetector.GetNormal(edge.From, edge.To);
                massPoint.Position = intersectPoint.Value;
                massPoint.Velocity = massPoint.Velocity - 2 * (massPoint.Velocity * normal) * normal;
                massPoint.Velocity *= Constants.Friction;
                massPoint.Position += massPoint.Velocity * Constants.TimeDelta;
            }
        }
    }

    private void ApplySoftBodyCollisions()
    {
        foreach (var (body1, body2) in _softBodiesCollection.SoftBodies.GetCartesianProduct())
        {
            foreach (var spring in body1.Springs)
            {
                foreach (var massPoint in body2.MassPoints)
                {
                    var intersectPoint = _segmentIntersector.GetIntersectPoint(spring.PointA.Position, spring.PointB.Position, massPoint.PrevPosition, massPoint.Position);
                    if (intersectPoint == null) continue;

                    spring.PointA.Velocity += massPoint.Velocity;
                    spring.PointB.Velocity += massPoint.Velocity;

                    var normal = _normalDetector.GetNormal(spring.PointA.Position, spring.PointB.Position);
                    massPoint.Position = intersectPoint.Value;
                    massPoint.Velocity = massPoint.Velocity - 2 * (massPoint.Velocity * normal) * normal;
                    massPoint.Velocity *= Constants.Friction;
                    massPoint.Position += massPoint.Velocity * Constants.TimeDelta;
                }
            }
        }
    }
}
