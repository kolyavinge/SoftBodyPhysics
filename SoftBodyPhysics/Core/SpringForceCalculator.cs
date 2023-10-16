namespace SoftBodyPhysics.Core;

internal interface ISpringForceCalculator
{
    void ApplySpringForce();
}

internal class SpringForceCalculator : ISpringForceCalculator
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IPhysicsUnits _physicsUnits;

    public SpringForceCalculator(
        ISoftBodiesCollection softBodiesCollection,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _physicsUnits = physicsUnits;
    }

    public void ApplySpringForce()
    {
        foreach (var spring in _softBodiesCollection.AllSprings)
        {
            var a = spring.PointA;
            var b = spring.PointB;
            var fs = spring.Stiffness * spring.DeformLength;
            var diffBA = (b.Position - a.Position).Normalized;
            var fd = _physicsUnits.SpringDamper * (diffBA * (b.Velocity - a.Velocity)); // (B.Position - A.Position).Normalized * (B.Velocity - A.Velocity)
            var f = fs + fd;
            var df = f * diffBA;
            a.Force += df; // (B.Position - A.Position).Normalized
            b.Force -= df; // (A.Position - B.Position).Normalized
        }
    }
}
