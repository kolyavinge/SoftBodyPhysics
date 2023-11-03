using System;
using SoftBodyPhysics.Model;

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
            ApplySpringForce(softBody.Springs);
        }
    }

    private void ApplySpringForce(Spring[] springs)
    {
        var springDamper = _physicsUnits.SpringDamper;
        for (int i = 0; i < springs.Length; i++)
        {
            var spring = springs[i];
            var a = spring.PointA;
            var b = spring.PointB;

            var positionDiffX = b.Position.x - a.Position.x;
            var positionDiffY = b.Position.y - a.Position.y;

            var positionDiffLength = (float)Math.Sqrt(positionDiffX * positionDiffX + positionDiffY * positionDiffY);
            var positionDiffUnitX = positionDiffX / positionDiffLength;
            var positionDiffUnitY = positionDiffY / positionDiffLength;

            var velocityDiffX = b.Velocity.x - a.Velocity.x;
            var velocityDiffY = b.Velocity.y - a.Velocity.y;

            // stiffness force
            var fs = spring.Stiffness * (positionDiffLength - spring.RestLength);

            // damper force
            // SpringDamper * (B.Position - A.Position).Unit * (B.Velocity - A.Velocity)
            var fd = springDamper * (positionDiffUnitX * velocityDiffX + positionDiffUnitY * velocityDiffY);

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
