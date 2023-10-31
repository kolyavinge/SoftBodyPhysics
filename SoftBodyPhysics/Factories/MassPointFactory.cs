using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Factories;

internal interface IMassPointFactory
{
    MassPoint Make(Vector position);
}

internal class MassPointFactory : IMassPointFactory
{
    private readonly IPhysicsUnits _physicsUnits;

    public MassPointFactory(IPhysicsUnits physicsUnits)
    {
        _physicsUnits = physicsUnits;
    }

    public MassPoint Make(Vector position)
    {
        var massPoint = new MassPoint(position)
        {
            Mass = _physicsUnits.Mass,
        };

        return massPoint;
    }
}
