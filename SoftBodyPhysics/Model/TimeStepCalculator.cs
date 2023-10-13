using System;

namespace SoftBodyPhysics.Model;

internal interface ITimeStepCalculator
{
    float GetTimeStepAndCorrectPositionSteps(float currentTimeStep);
}

internal class TimeStepCalculator : ITimeStepCalculator
{
    private readonly ISoftBodiesCollection _softBodiesCollection;

    public TimeStepCalculator(ISoftBodiesCollection softBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
    }

    public float GetTimeStepAndCorrectPositionSteps(float currentTimeStep)
    {
        var max = 0.0f;
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            max = Math.Max(max, massPoint.PositionStep.Length);
        }
        var aspect = max / Constants.MassPointRadius;
        var iaspect = 1.0f / aspect;
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.PositionStep *= iaspect;
        }

        return currentTimeStep / aspect;
    }
}
