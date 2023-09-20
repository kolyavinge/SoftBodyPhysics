using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Factories;

internal interface IHardBodyFactory
{
    HardBody Make();
}

internal class HardBodyFactory : IHardBodyFactory
{
    public HardBody Make()
    {
        return new HardBody();
    }
}
