namespace SoftBodyPhysics.Core;

internal interface IGravityForceCalculator
{
    void InitGravityForce();
}

internal class GravityForceCalculator : IGravityForceCalculator
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IPhysicsUnits _physicsUnits;

    public GravityForceCalculator(
        ISoftBodiesCollection softBodiesCollection,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _physicsUnits = physicsUnits;
    }

    public void InitGravityForce()
    {
        var gravityAcceleration = -_physicsUnits.GravityAcceleration;
        var softBodies = _softBodiesCollection.SoftBodies;
        for (int i = 0; i < softBodies.Length; i++)
        {
            var softBody = softBodies[i];
            if (!softBody.IsActive) continue;
            for (int j = 0; j < softBody.MassPoints.Length; j++)
            {
                var massPoint = softBody.MassPoints[j];
                massPoint.Force.x = 0;
                massPoint.Force.y = gravityAcceleration * massPoint.Mass;
            }
        }
    }
}
