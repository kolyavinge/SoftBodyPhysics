using DependencyInjection;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Factories;

public static class PhysicsWorldFactory
{
    public static IPhysicsWorld Make()
    {
        var container = DependencyContainerFactory.MakeLiteContainer();
        container.InitFromModules(new MainInjectModule());

        return container.Resolve<IPhysicsWorld>();
    }
}
