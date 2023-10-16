namespace SoftBodyPhysics.Core;

internal interface IVelocityCalculator
{
    void ApplyVelocity(float timeStep);
}

internal class VelocityCalculator : IVelocityCalculator
{
    private readonly ISoftBodiesCollection _softBodiesCollection;

    public VelocityCalculator(ISoftBodiesCollection softBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
    }

    public void ApplyVelocity(float timeStep)
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Velocity += massPoint.Force * (timeStep / massPoint.Mass);
            massPoint.PositionStep = massPoint.Velocity * timeStep;
        }
    }
}
