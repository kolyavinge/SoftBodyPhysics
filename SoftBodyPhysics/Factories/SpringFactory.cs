using SoftBodyPhysics.Core;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Factories;

internal interface ISpringFactory
{
    Spring Make(MassPoint a, MassPoint b);
}

internal class SpringFactory : ISpringFactory
{
    private readonly IPhysicsUnits _physicsUnits;

    public SpringFactory(IPhysicsUnits physicsUnits)
    {
        _physicsUnits = physicsUnits;
    }

    public Spring Make(MassPoint a, MassPoint b)
    {
        var spring = new Spring(a, b)
        {
            Stiffness = _physicsUnits.SpringStiffness
        };

        return spring;
    }
}
