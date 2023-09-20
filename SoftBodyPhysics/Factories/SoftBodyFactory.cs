using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Factories;

internal interface ISoftBodyFactory
{
    SoftBody Make();
}

internal class SoftBodyFactory : ISoftBodyFactory
{
    private readonly IPhysicsUnits _physicsUnits;

    public SoftBodyFactory(IPhysicsUnits physicsUnits)
    {
        _physicsUnits = physicsUnits;
    }

    public SoftBody Make()
    {
        return new SoftBody(_physicsUnits);
    }
}
