using System;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface ISoftBodyActivator
{
    void Activate();
}

internal class SoftBodyActivator : ISoftBodyActivator
{
    private const float _positionDelta = 0.1f;
    private const float _velocityDelta = 0.2f;
    private readonly ISoftBodiesCollection _softBodiesCollection;

    public SoftBodyActivator(
        ISoftBodiesCollection softBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
    }

    public void Activate()
    {
        var softBodies = _softBodiesCollection.SoftBodies;
        for (int i = 0; i < softBodies.Length; i++)
        {
            var softBody = softBodies[i];
            softBody.IsActive = IsActive(softBody.EdgeMassPoints);
        }
    }

    private bool IsActive(MassPoint[] massPoints)
    {
        for (int i = 0; i < massPoints.Length; i++)
        {
            var massPoint = massPoints[i];
            if (MathF.Abs(massPoint.Position.x - massPoint.PositionBeforeUpdate.x) >= _positionDelta) return true;
            if (MathF.Abs(massPoint.Position.y - massPoint.PositionBeforeUpdate.y) >= _positionDelta) return true;
            if (MathF.Abs(massPoint.Velocity.x - massPoint.VelocityBeforeUpdate.x) >= _velocityDelta) return true;
            if (MathF.Abs(massPoint.Velocity.y - massPoint.VelocityBeforeUpdate.y) >= _velocityDelta) return true;
        }

        return false;
    }
}
