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
            spring.PointA.Position = spring.PointA.PrevPosition;
            spring.PointB.Position = spring.PointB.PrevPosition;
            spring.PointA.Velocity = _physicsUnits.Sliding * _vectorCalculator.GetReflectedVector(spring.PointA.Velocity, normal);
            spring.PointB.Velocity = _physicsUnits.Sliding * _vectorCalculator.GetReflectedVector(spring.PointB.Velocity, normal);

            massPoint.Collision = spring;
            massPoint.Position = massPoint.PrevPosition;
            massPoint.Velocity = _physicsUnits.Sliding * _vectorCalculator.GetReflectedVector(massPoint.Velocity, normal);

            return;
        }
    }
}
