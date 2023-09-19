using DependencyInjection;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Utils;

internal class MainInjectModule : InjectModule
{
    public override void Init(IBindingProvider bindingProvider)
    {
        bindingProvider.Bind<ILineIntersector, LineIntersector>().ToSingleton();
        bindingProvider.Bind<ISegmentIntersector, SegmentIntersector>().ToSingleton();
        bindingProvider.Bind<INormalDetector, NormalDetector>().ToSingleton();
        bindingProvider.Bind<ISoftBodiesCollection, SoftBodiesCollection>().ToSingleton();
        bindingProvider.Bind<IHardBodiesCollection, HardBodiesCollection>().ToSingleton();
        bindingProvider.Bind<IPhysicsWorldUpdater, PhysicsWorldUpdater>().ToSingleton();
        bindingProvider.Bind<IPhysicsWorld, PhysicsWorld>().ToSingleton();
    }
}
