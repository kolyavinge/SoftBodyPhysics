using System;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface ISoftBodyMovingDetector
{
    void DetectMoving();
}

internal class SoftBodyMovingDetector : ISoftBodyMovingDetector
{
    private const float _delta = 0.1f;
    private readonly ISoftBodiesCollection _softBodiesCollection;

    public SoftBodyMovingDetector(
        ISoftBodiesCollection softBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
    }

    public void DetectMoving()
    {
        var softBodies = _softBodiesCollection.SoftBodies;
        for (int i = 0; i < softBodies.Length; i++)
        {
            var softBody = softBodies[i];
            softBody.IsMoving = IsMoving(softBody.MassPoints);
        }
    }

    private bool IsMoving(MassPoint[] massPoints)
    {
        for (int i = 0; i < massPoints.Length; i++)
        {
            var massPoint = massPoints[i];
            if (Math.Abs(massPoint.Velocity.x) >= _delta) return true;
            if (Math.Abs(massPoint.Velocity.y) >= _delta) return true;
        }

        return false;
    }
}
