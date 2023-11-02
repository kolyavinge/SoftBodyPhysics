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
            var len = massPoint.PositionStep.Length2;
            if (len > max) max = len;
        }

        var aspect = (float)Math.Sqrt(max) / Constants.MassPointRadius;
        var newTimeStep = currentTimeStep / aspect;

        return newTimeStep > currentTimeStep ? currentTimeStep : newTimeStep;
    }
}
