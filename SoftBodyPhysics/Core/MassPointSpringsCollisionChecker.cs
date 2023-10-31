using SoftBodyPhysics.Calculations;
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
    private readonly IPhysicsUnits _physicsUnits;

    public MassPointSpringsCollisionChecker(
        ISegmentIntersectDetector segmentIntersectDetector,
        IPhysicsUnits physicsUnits)
    {
        _segmentIntersectDetector = segmentIntersectDetector;
        _physicsUnits = physicsUnits;
    }

    public void CheckMassPointAndSpringsCollision(MassPoint massPoint, Spring[] springs)
    {
        for (var i = 0; i < springs.Length; i++)
        {
            var spring = springs[i];
            if (!_segmentIntersectDetector.Intersected(spring.PointA.Position, spring.PointB.Position, massPoint.Position)) continue;

            var (massPointNewVelocity, springNewVelocity) = PhysCalcs.GetNewVelocityAfterCollision(
                massPoint.Mass,
                massPoint.Velocity,
                0.5f * (spring.PointA.Mass + spring.PointB.Mass),
                0.5f * (spring.PointA.Velocity + spring.PointB.Velocity));

            spring.Collisions.Add(massPoint);
            massPoint.Collision = spring;
            ApplyPositionAndVelocity(spring, springNewVelocity);
            ApplyPositionAndVelocity(massPoint, massPointNewVelocity);

            return;
        }
    }

    private void ApplyPositionAndVelocity(Spring spring, Vector springNewVelocity)
    {
        spring.PointA.Position.x = spring.PointA.PrevPosition.x;
        spring.PointA.Position.y = spring.PointA.PrevPosition.y;
        spring.PointB.Position.x = spring.PointB.PrevPosition.x;
        spring.PointB.Position.y = spring.PointB.PrevPosition.y;

        spring.PointA.Velocity.x = springNewVelocity.x;
        spring.PointA.Velocity.y = springNewVelocity.y;
        spring.PointB.Velocity.x = springNewVelocity.x;
        spring.PointB.Velocity.y = springNewVelocity.y;

        spring.PointA.Velocity.x *= _physicsUnits.Sliding;
        spring.PointA.Velocity.y *= _physicsUnits.Sliding;
        spring.PointB.Velocity.x *= _physicsUnits.Sliding;
        spring.PointB.Velocity.y *= _physicsUnits.Sliding;
    }

    private void ApplyPositionAndVelocity(MassPoint massPoint, Vector massPointNewVelocity)
    {
        massPoint.Position.x = massPoint.PrevPosition.x;
        massPoint.Position.y = massPoint.PrevPosition.y;

        massPoint.Velocity.x = massPointNewVelocity.x;
        massPoint.Velocity.y = massPointNewVelocity.y;

        massPoint.Velocity.x *= _physicsUnits.Sliding;
        massPoint.Velocity.y *= _physicsUnits.Sliding;
    }
}
