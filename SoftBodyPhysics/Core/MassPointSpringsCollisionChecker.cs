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
    private readonly IVectorCalculator _vectorCalculator;
    private readonly IPhysicsUnits _physicsUnits;

    public MassPointSpringsCollisionChecker(
        ISegmentIntersectDetector segmentIntersectDetector,
        IVectorCalculator vectorCalculator,
        IPhysicsUnits physicsUnits)
    {
        _segmentIntersectDetector = segmentIntersectDetector;
        _vectorCalculator = vectorCalculator;
        _physicsUnits = physicsUnits;
    }

    public void CheckMassPointAndSpringsCollision(MassPoint massPoint, Spring[] springs)
    {
        foreach (var spring in springs)
        {
            if (!_segmentIntersectDetector.Intersected(spring.PointA.Position, spring.PointB.Position, massPoint.Position)) continue;

            var normal = _vectorCalculator.GetNormalVector(spring.PointA.Position, spring.PointB.Position);

            spring.Collisions.Add(massPoint);
            spring.PointA.Position.x = spring.PointA.PrevPosition.x;
            spring.PointA.Position.y = spring.PointA.PrevPosition.y;
            spring.PointB.Position.x = spring.PointB.PrevPosition.x;
            spring.PointB.Position.y = spring.PointB.PrevPosition.y;

            _vectorCalculator.ReflectVector(spring.PointA.Velocity, normal);
            spring.PointA.Velocity.x *= _physicsUnits.Sliding;
            spring.PointA.Velocity.y *= _physicsUnits.Sliding;

            _vectorCalculator.ReflectVector(spring.PointB.Velocity, normal);
            spring.PointB.Velocity.x *= _physicsUnits.Sliding;
            spring.PointB.Velocity.y *= _physicsUnits.Sliding;

            massPoint.Collision = spring;
            massPoint.Position.x = massPoint.PrevPosition.x;
            massPoint.Position.y = massPoint.PrevPosition.y;

            _vectorCalculator.ReflectVector(massPoint.Velocity, normal);
            massPoint.Velocity.x *= _physicsUnits.Sliding;
            massPoint.Velocity.y *= _physicsUnits.Sliding;

            return;
        }
    }
}
