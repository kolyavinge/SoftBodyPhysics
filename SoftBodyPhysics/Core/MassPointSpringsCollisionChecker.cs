using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Intersections;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IMassPointSpringsCollisionChecker
{
    void CheckMassPointAndSpringsCollision(MassPoint massPoint, Spring[] springs);
}

internal class MassPointSpringsCollisionChecker : IMassPointSpringsCollisionChecker
{
    private readonly ISegmentIntersectDetector _segmentIntersectDetector;
    private readonly INormalCalculator _normalCalculator;
    private readonly IPhysicsUnits _physicsUnits;

    public MassPointSpringsCollisionChecker(
        ISegmentIntersectDetector segmentIntersectDetector,
        INormalCalculator normalCalculator,
        IPhysicsUnits physicsUnits)
    {
        _segmentIntersectDetector = segmentIntersectDetector;
        _normalCalculator = normalCalculator;
        _physicsUnits = physicsUnits;
    }

    public void CheckMassPointAndSpringsCollision(MassPoint massPoint, Spring[] springs)
    {
        foreach (var spring in springs)
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
