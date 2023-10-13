using System;
using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Intersections;

namespace SoftBodyPhysics.Model;

internal interface ISoftBodyCollisionChecker
{
    void CheckCollisions(SoftBody body);
}

internal class SoftBodyCollisionChecker : ISoftBodyCollisionChecker
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly ISegmentIntersectDetector _segmentIntersectDetector;
    private readonly INormalCalculator _normalCalculator;
    private readonly IPhysicsUnits _physicsUnits;

    public SoftBodyCollisionChecker(
        ISoftBodiesCollection softBodiesCollection,
        ISegmentIntersectDetector segmentIntersectDetector,
        INormalCalculator normalCalculator,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _segmentIntersectDetector = segmentIntersectDetector;
        _normalCalculator = normalCalculator;
        _physicsUnits = physicsUnits;
    }

    public void CheckCollisions(SoftBody body)
    {
        var delta = 2.0f;
        foreach (var body2 in _softBodiesCollection.SoftBodies)
        {
            if (body == body2) continue;

            if (Math.Abs(body.Borders.MiddleX - body2.Borders.MiddleX) > body.Borders.Width + body2.Borders.Width) continue;
            if (Math.Abs(body.Borders.MiddleY - body2.Borders.MiddleY) > body.Borders.Height + body2.Borders.Height) continue;

            foreach (var massPoint in body.MassPoints)
            {
                if (body2.Borders.MinX - delta < massPoint.Position.X && massPoint.Position.X < body2.Borders.MaxX + delta &&
                    body2.Borders.MinY - delta < massPoint.Position.Y && massPoint.Position.Y < body2.Borders.MaxY + delta)
                {
                    CheckMassPointAndSpringsCollision(massPoint, body2.SpringsToCheckCollisions);
                }
            }

            foreach (var massPoint in body2.MassPoints)
            {
                if (body.Borders.MinX - delta < massPoint.Position.X && massPoint.Position.X < body.Borders.MaxX + delta &&
                    body.Borders.MinY - delta < massPoint.Position.Y && massPoint.Position.Y < body.Borders.MaxY + delta)
                {
                    CheckMassPointAndSpringsCollision(massPoint, body.SpringsToCheckCollisions);
                }
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

            spring.PointA.Velocity -= 2.0f * (spring.PointA.Velocity * normal) * normal; // reflected vectors
            spring.PointB.Velocity -= 2.0f * (spring.PointB.Velocity * normal) * normal;
            spring.PointA.Velocity *= 1.0f - _physicsUnits.Friction;
            spring.PointB.Velocity *= 1.0f - _physicsUnits.Friction;

            massPoint.State = CollisionState.Collision;
            massPoint.Position = massPoint.PrevPosition;
            massPoint.Velocity -= 2.0f * (massPoint.Velocity * normal) * normal; // reflected vector
            massPoint.Velocity *= 1.0f - _physicsUnits.Friction;

            return;
        }
    }
}
