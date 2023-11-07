namespace SoftBodyPhysics.Core;

internal interface ITimeStepCalculator
{
    float GetTimeStep(float currentTimeStep, float maxPositionStep);
}

internal class TimeStepCalculator : ITimeStepCalculator
{
    public float GetTimeStep(float currentTimeStep, float maxPositionStep)
    {
        var aspect = maxPositionStep / Constants.MassPointRadius;
        var newTimeStep = currentTimeStep / aspect;

        return newTimeStep > currentTimeStep ? currentTimeStep : newTimeStep;
    }
}
