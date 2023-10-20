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
        foreach (var spring in _softBodiesCollection.AllSprings)
        {
            var a = spring.PointA;
            var b = spring.PointB;

            var positionDiffX = b.Position.X - a.Position.X;
            var positionDiffY = b.Position.Y - a.Position.Y;

            //if (positionDiffX == 0 && positionDiffY == 0) continue;

            var positionDiffLength = (float)Math.Sqrt(positionDiffX * positionDiffX + positionDiffY * positionDiffY);
            var positionDiffUnitX = positionDiffX / positionDiffLength;
            var positionDiffUnitY = positionDiffY / positionDiffLength;

            var velocityDiffX = b.Velocity.X - a.Velocity.X;
            var velocityDiffY = b.Velocity.Y - a.Velocity.Y;

            // stiffness force
            var fs = spring.Stiffness * (positionDiffLength - spring.RestLength);

            // damper force
            // SpringDamper * (B.Position - A.Position).Unit * (B.Velocity - A.Velocity)
            var fd = _physicsUnits.SpringDamper * (positionDiffUnitX * velocityDiffX + positionDiffUnitY * velocityDiffY);

            // total force
            var f = fs + fd;
            var dfX = f * positionDiffUnitX;
            var dfY = f * positionDiffUnitY;

            a.Force.X += dfX;
            a.Force.Y += dfY;

            b.Force.X -= dfX;
            b.Force.Y -= dfY;
        }
    }
}
