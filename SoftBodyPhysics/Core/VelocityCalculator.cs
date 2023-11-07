using System;

namespace SoftBodyPhysics.Core;

internal interface IVelocityCalculator
{
    float GetMaxPositionStep(float timeStep);
    void ApplyVelocity(float timeStep);
}

internal class VelocityCalculator : IVelocityCalculator
{
    private readonly ISoftBodiesCollection _softBodiesCollection;

    public VelocityCalculator(ISoftBodiesCollection softBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
    }

    public float GetMaxPositionStep(float timeStep)
    {
        var maxPositionStepLengthSquared = 0.0f;
        var softBodies = _softBodiesCollection.ActivatedSoftBodies;
        var count = _softBodiesCollection.ActivatedSoftBodiesCount;
        for (int i = 0; i < count; i++)
        {
            var softBody = softBodies[i];

            for (var j = 0; j < softBody.MassPoints.Length; j++)
            {
                var massPoint = softBody.MassPoints[j];

                var tsd = timeStep / massPoint.Mass;

                var newVelocityX = massPoint.Velocity.x + massPoint.Force.x * tsd;
                var newVelocityY = massPoint.Velocity.y + massPoint.Force.y * tsd;

                var positionStepX = newVelocityX * timeStep;
                var positionStepY = newVelocityY * timeStep;
                var positionStepLengthSquared = positionStepX * positionStepX + positionStepY * positionStepY;
                if (positionStepLengthSquared > maxPositionStepLengthSquared) maxPositionStepLengthSquared = positionStepLengthSquared;
            }
        }

        return MathF.Sqrt(maxPositionStepLengthSquared);
    }

    public void ApplyVelocity(float timeStep)
    {
        var softBodies = _softBodiesCollection.ActivatedSoftBodies;
        var count = _softBodiesCollection.ActivatedSoftBodiesCount;
        for (int i = 0; i < count; i++)
        {
            var softBody = softBodies[i];

            for (var j = 0; j < softBody.MassPoints.Length; j++)
            {
                var massPoint = softBody.MassPoints[j];

                var tsd = timeStep / massPoint.Mass;

                massPoint.Velocity.x += massPoint.Force.x * tsd;
                massPoint.Velocity.y += massPoint.Force.y * tsd;

                massPoint.PositionStep.x = massPoint.Velocity.x * timeStep;
                massPoint.PositionStep.y = massPoint.Velocity.y * timeStep;
            }
        }
    }
}
