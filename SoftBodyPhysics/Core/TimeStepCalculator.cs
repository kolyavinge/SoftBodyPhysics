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

        var allMassPoints = _softBodiesCollection.AllMassPoints;
        for (var i = 0; i < allMassPoints.Length; i++)
        {
            var massPoint = allMassPoints[i];
            max = Math.Max(max, massPoint.PositionStep.Length);
        }

        var aspect = max / Constants.MassPointRadius;
        var newTimeStep = currentTimeStep / aspect;

        return newTimeStep > currentTimeStep ? currentTimeStep : newTimeStep;
    }
}
