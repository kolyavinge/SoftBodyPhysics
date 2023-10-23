using System;

namespace SoftBodyPhysics.Core;

internal interface ISpringForceCalculator
{
    void ApplySpringForce();
}

internal class SpringForceCalculator : ISpringForceCalculator
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IPhysicsUnits _physicsUnits;

    public SpringForceCalculator(
        ISoftBodiesCollection softBodiesCollection,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _physicsUnits = physicsUnits;
    }

    public void ApplySpringForce()
    {
        var softBodies = _softBodiesCollection.SoftBodies;
        for (int i = 0; i < softBodies.Length; i++)
        {
            var softBody = softBodies[i];
            if (!softBody.IsMoving) continue;
            var springs = softBody.Springs;
            for (int j = 0; j < springs.Length; j++)
            {
                var spring = springs[j];
                var a = spring.PointA;
                var b = spring.PointB;

                var positionDiffX = b.Position.x - a.Position.x;
                var positionDiffY = b.Position.y - a.Position.y;

                //if (positionDiffX == 0 && positionDiffY == 0) continue;

                var positionDiffLength = (float)Math.Sqrt(positionDiffX * positionDiffX + positionDiffY * positionDiffY);
                var positionDiffUnitX = positionDiffX / positionDiffLength;
                var positionDiffUnitY = positionDiffY / positionDiffLength;

                var velocityDiffX = b.Velocity.x - a.Velocity.x;
                var velocityDiffY = b.Velocity.y - a.Velocity.y;

                // stiffness force
                var fs = spring.Stiffness * (positionDiffLength - spring.RestLength);

                // damper force
                // SpringDamper * (B.Position - A.Position).Unit * (B.Velocity - A.Velocity)
                var fd = _physicsUnits.SpringDamper * (positionDiffUnitX * velocityDiffX + positionDiffUnitY * velocityDiffY);

                // total force
                var f = fs + fd;
                var dfX = f * positionDiffUnitX;
                var dfY = f * positionDiffUnitY;

                a.Force.x += dfX;
                a.Force.y += dfY;

                b.Force.x -= dfX;
                b.Force.y -= dfY;
            }
        }
    }
}
