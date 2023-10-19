using System;

namespace SoftBodyPhysics.Core;

internal interface ITimeStepCalculator
{
    float GetTimeStep(float currentTimeStep);
}

internal class TimeStepCalculator : ITimeStepCalculator
{
    private readonly ISoftBodiesCollection _softBodiesCollection;

    public TimeStepCalculator(
        ISoftBodiesCollection softBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
    }

    public float GetTimeStep(float currentTimeStep)
    {
        var max = 0.0f;
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            max = Math.Max(max, massPoint.PositionStep.Length);
        }
        var aspect = max / Constants.MassPointRadius;
        var newTimeStep = currentTimeStep / aspect;

        return newTimeStep > currentTimeStep ? currentTimeStep : newTimeStep;
    }
}
