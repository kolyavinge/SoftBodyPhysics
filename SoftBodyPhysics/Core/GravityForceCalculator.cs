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
        var allMassPoints = _softBodiesCollection.AllMassPoints;
        for (var i = 0; i < allMassPoints.Length; i++)
        {
            var massPoint = allMassPoints[i];
            massPoint.Force.x = 0;
            massPoint.Force.y = -_physicsUnits.GravityAcceleration * massPoint.Mass;
        }
    }
}
