using DependencyInjection;
using SoftBodyPhysics.Ancillary;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Intersections;

namespace SoftBodyPhysics.Utils;

internal class MainInjectModule : InjectModule
{
    public override void Init(IBindingProvider bindingProvider)
    {
        bindingProvider.Bind<IPhysicsUnits, PhysicsUnits>().ToSingleton();
        bindingProvider.Bind<IVectorCalculator, VectorCalculator>().ToSingleton();
        bindingProvider.Bind<ILineIntersector, LineIntersector>().ToSingleton();
        bindingProvider.Bind<ISegmentIntersector, SegmentIntersector>().ToSingleton();
        bindingProvider.Bind<IPolygonIntersector, PolygonIntersector>().ToSingleton();
        bindingProvider.Bind<ISoftBodyIntersector, SoftBodyIntersector>().ToSingleton();
        bindingProvider.Bind<ISoftBodiesCollection, SoftBodiesCollection>().ToSingleton();
        bindingProvider.Bind<IHardBodiesCollection, HardBodiesCollection>().ToSingleton();
        bindingProvider.Bind<IMassPointFactory, MassPointFactory>().ToSingleton();
        bindingProvider.Bind<ISpringFactory, SpringFactory>().ToSingleton();
        bindingProvider.Bind<IEdgeFactory, EdgeFactory>().ToSingleton();
        bindingProvider.Bind<ISoftBodyFactory, SoftBodyFactory>().ToSingleton();
        bindingProvider.Bind<IHardBodyFactory, HardBodyFactory>().ToSingleton();
        bindingProvider.Bind<IBordersUpdater, BordersUpdater>().ToSingleton();
        bindingProvider.Bind<ISoftBodyMovingDetector, SoftBodyMovingDetector>().ToSingleton();
        bindingProvider.Bind<ISoftBodySpringEdgeDetector, SoftBodySpringEdgeDetector>().ToSingleton();
        bindingProvider.Bind<IBodyEditorFactory, BodyEditorFactory>().ToSingleton();
        bindingProvider.Bind<IBodyBordersUpdater, BodyBordersUpdater>().ToSingleton();
        bindingProvider.Bind<IGravityForceCalculator, GravityForceCalculator>().ToSingleton();
        bindingProvider.Bind<IPhysicsWorldFrameInitializer, PhysicsWorldFrameInitializer>().ToSingleton();
        bindingProvider.Bind<ISpringForceCalculator, SpringForceCalculator>().ToSingleton();
        bindingProvider.Bind<IVelocityCalculator, VelocityCalculator>().ToSingleton();
        bindingProvider.Bind<ITimeStepCalculator, TimeStepCalculator>().ToSingleton();
        bindingProvider.Bind<IMassPointSpringsCollisionChecker, MassPointSpringsCollisionChecker>().ToSingleton();
        bindingProvider.Bind<IMassPointEdgeCollisionChecker, MassPointEdgeCollisionChecker>().ToSingleton();
        bindingProvider.Bind<ISoftBodyCollisionChecker, SoftBodyCollisionChecker>().ToSingleton();
        bindingProvider.Bind<IHardBodyCollisionChecker, HardBodyCollisionChecker>().ToSingleton();
        bindingProvider.Bind<ICollisionChecker, CollisionChecker>().ToSingleton();
        bindingProvider.Bind<IPhysicsWorldUpdater, PhysicsWorldUpdater>().ToSingleton();
        bindingProvider.Bind<IPhysicsWorld, PhysicsWorld>().ToSingleton();
    }
}
