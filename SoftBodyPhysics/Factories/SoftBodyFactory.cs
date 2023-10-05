using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Factories;

internal interface ISoftBodyFactory
{
    SoftBody Make();
}

internal class SoftBodyFactory : ISoftBodyFactory
{
    public SoftBody Make()
    {
        return new SoftBody();
    }
}
