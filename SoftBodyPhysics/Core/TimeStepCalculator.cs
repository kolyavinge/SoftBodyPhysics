using System;

namespace SoftBodyPhysics.Core;

internal interface ITimeStepCalculator
{
    float GetTimeStep(float currentTimeStep);
}

internal class TimeStepCalculator : ITimeStepCalculator
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IPhysicsUnits _physicsUnits;

    public TimeStepCalculator(
        ISoftBodiesCollection softBodiesCollection,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _physicsUnits = physicsUnits;
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

        return newTimeStep > _physicsUnits.Time ? _physicsUnits.Time : newTimeStep;
    }
}
