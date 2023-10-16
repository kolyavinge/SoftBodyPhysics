using SoftBodyPhysics.Geo;

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
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Force = new Vector(0, -_physicsUnits.GravityAcceleration * massPoint.Mass);
        }
    }
}
