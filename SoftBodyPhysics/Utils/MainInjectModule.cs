using DependencyInjection;
using SoftBodyPhysics.Ancillary;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Intersections;

namespace SoftBodyPhysics.Utils;

internal class MainInjectModule : InjectModule
{
    public override void Init(IBindingProvider bindingProvider)
    {
        bindingProvider.Bind<IPhysicsUnits, PhysicsUnits>().ToSingleton();
        bindingProvider.Bind<INormalCalculator, NormalCalculator>().ToSingleton();
        bindingProvider.Bind<ILineIntersector, LineIntersector>().ToSingleton();
        bindingProvider.Bind<ISegmentChecker, SegmentChecker>().ToSingleton();
        bindingProvider.Bind<ISegmentIntersector, SegmentIntersector>().ToSingleton();
        bindingProvider.Bind<ISegmentIntersectDetector, SegmentIntersectDetector>().ToSingleton();
        bindingProvider.Bind<IPolygonChecker, PolygonChecker>().ToSingleton();
        bindingProvider.Bind<ISoftBodyIntersector, SoftBodyIntersector>().ToSingleton();
        bindingProvider.Bind<ISoftBodiesCollection, SoftBodiesCollection>().ToSingleton();
        bindingProvider.Bind<IHardBodiesCollection, HardBodiesCollection>().ToSingleton();
        bindingProvider.Bind<IMassPointFactory, MassPointFactory>().ToSingleton();
        bindingProvider.Bind<ISpringFactory, SpringFactory>().ToSingleton();
        bindingProvider.Bind<IEdgeFactory, EdgeFactory>().ToSingleton();
        bindingProvider.Bind<ISoftBodyFactory, SoftBodyFactory>().ToSingleton();
        bindingProvider.Bind<IHardBodyFactory, HardBodyFactory>().ToSingleton();
        bindingProvider.Bind<IBordersCalculator, BordersCalculator>().ToSingleton();
        bindingProvider.Bind<ISoftBodySpringEdgeDetector, SoftBodySpringEdgeDetector>().ToSingleton();
        bindingProvider.Bind<IBodyEditorFactory, BodyEditorFactory>().ToSingleton();
        bindingProvider.Bind<ISoftBodyBordersUpdater, SoftBodyBordersUpdater>().ToSingleton();
        bindingProvider.Bind<IHardBodyBordersUpdater, HardBodyBordersUpdater>().ToSingleton();
        bindingProvider.Bind<ITimeStepCalculator, TimeStepCalculator>().ToSingleton();
        bindingProvider.Bind<ISoftBodyCollisionChecker, SoftBodyCollisionChecker>().ToSingleton();
        bindingProvider.Bind<IHardBodyCollisionChecker, HardBodyCollisionChecker>().ToSingleton();
        bindingProvider.Bind<IPhysicsWorldUpdater, PhysicsWorldUpdater>().ToSingleton();
        bindingProvider.Bind<IPhysicsWorld, PhysicsWorld>().ToSingleton();
    }
}
