namespace SoftBodyPhysics.Core;

internal interface IVelocityCalculator
{
    void CalculatePositionStep(float timeStep);
    void ApplyVelocity(float timeStep);
}

internal class VelocityCalculator : IVelocityCalculator
{
    private readonly ISoftBodiesCollection _softBodiesCollection;

    public VelocityCalculator(ISoftBodiesCollection softBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
    }

    public void CalculatePositionStep(float timeStep)
    {
        var allMassPoints = _softBodiesCollection.AllMassPoints;
        for (var i = 0; i < allMassPoints.Length; i++)
        {
            var massPoint = allMassPoints[i];

            var tsd = timeStep / massPoint.Mass;

            var newVelocityX = massPoint.Velocity.x + massPoint.Force.x * tsd;
            var newVelocityY = massPoint.Velocity.y + massPoint.Force.y * tsd;

            massPoint.PositionStep.x = newVelocityX * timeStep;
            massPoint.PositionStep.y = newVelocityY * timeStep;
        }
    }

    public void ApplyVelocity(float timeStep)
    {
        var allMassPoints = _softBodiesCollection.AllMassPoints;
        for (var i = 0; i < allMassPoints.Length; i++)
        {
            var massPoint = allMassPoints[i];

            var tsd = timeStep / massPoint.Mass;

            massPoint.Velocity.x += massPoint.Force.x * tsd;
            massPoint.Velocity.y += massPoint.Force.y * tsd;

            massPoint.PositionStep.x = massPoint.Velocity.x * timeStep;
            massPoint.PositionStep.y = massPoint.Velocity.y * timeStep;
        }
    }
}
