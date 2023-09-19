using DependencyInjection;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

public static class PhysicsWorldFactory
{
    public static IPhysicsWorld Make()
    {
        var container = DependencyContainerFactory.MakeLiteContainer();
        container.InitFromModules(new MainInjectModule());

        return container.Resolve<IPhysicsWorld>();
    }
}
