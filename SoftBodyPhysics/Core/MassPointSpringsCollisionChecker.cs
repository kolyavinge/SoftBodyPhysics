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
    private readonly ISegmentIntersector _segmentIntersector;
    private readonly IPhysicsUnits _physicsUnits;

    public MassPointSpringsCollisionChecker(
        ISegmentIntersector segmentIntersector,
        IPhysicsUnits physicsUnits)
    {
        _segmentIntersector = segmentIntersector;
        _physicsUnits = physicsUnits;
    }

    public void CheckMassPointAndSpringsCollision(MassPoint massPoint, Spring[] springs)
    {
        for (var i = 0; i < springs.Length; i++)
        {
            var spring = springs[i];

            if (!_segmentIntersector.IsIntersected(spring.PointA.Position, spring.PointB.Position, massPoint.Position)) continue;

            var (massPointNewVelocityX, massPointNewVelocityY, springNewVelocityX, springNewVelocityY) =
                PhysCalcs.GetNewVelocityAfterCollision(
                    massPoint.Mass,
                    massPoint.Velocity.x,
                    massPoint.Velocity.y,
                    (spring.PointA.Mass + spring.PointB.Mass) / 2.0f,
                    (spring.PointA.Velocity.x + spring.PointB.Velocity.x) / 2.0f,
                    (spring.PointA.Velocity.y + spring.PointB.Velocity.y) / 2.0f);

            spring.Collisions.Add(massPoint);
            massPoint.Collision = spring;

            ApplyPositionAndVelocity(spring, springNewVelocityX, springNewVelocityY);
            ApplyPositionAndVelocity(massPoint, massPointNewVelocityX, massPointNewVelocityY);

            return;
        }
    }

    private void ApplyPositionAndVelocity(Spring spring, float springNewVelocityX, float springNewVelocityY)
    {
        spring.PointA.Position.x = spring.PointA.PrevPosition.x;
        spring.PointA.Position.y = spring.PointA.PrevPosition.y;
        spring.PointB.Position.x = spring.PointB.PrevPosition.x;
        spring.PointB.Position.y = spring.PointB.PrevPosition.y;

        spring.PointA.Velocity.x = springNewVelocityX;
        spring.PointA.Velocity.y = springNewVelocityY;
        spring.PointB.Velocity.x = springNewVelocityX;
        spring.PointB.Velocity.y = springNewVelocityY;

        spring.PointA.Velocity.x *= _physicsUnits.Sliding;
        spring.PointA.Velocity.y *= _physicsUnits.Sliding;
        spring.PointB.Velocity.x *= _physicsUnits.Sliding;
        spring.PointB.Velocity.y *= _physicsUnits.Sliding;
    }

    private void ApplyPositionAndVelocity(MassPoint massPoint, float massPointNewVelocityX, float massPointNewVelocityY)
    {
        massPoint.Position.x = massPoint.PrevPosition.x;
        massPoint.Position.y = massPoint.PrevPosition.y;

        massPoint.Velocity.x = massPointNewVelocityX;
        massPoint.Velocity.y = massPointNewVelocityY;

        massPoint.Velocity.x *= _physicsUnits.Sliding;
        massPoint.Velocity.y *= _physicsUnits.Sliding;
    }
}
