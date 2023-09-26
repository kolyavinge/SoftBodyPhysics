﻿using DependencyInjection;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Utils;

internal class MainInjectModule : InjectModule
{
    public override void Init(IBindingProvider bindingProvider)
    {
        bindingProvider.Bind<IPhysicsUnits, PhysicsUnits>().ToSingleton();
        bindingProvider.Bind<INormalCalculator, NormalCalculator>().ToSingleton();
        bindingProvider.Bind<ILineIntersector, LineIntersector>().ToSingleton();
        bindingProvider.Bind<ISegmentDetector, SegmentDetector>().ToSingleton();
        bindingProvider.Bind<ISegmentIntersector, SegmentIntersector>().ToSingleton();
        bindingProvider.Bind<ISpringIntersector, SpringIntersector>().ToSingleton();
        bindingProvider.Bind<ISoftBodyIntersector, SoftBodyIntersector>().ToSingleton();
        bindingProvider.Bind<ISoftBodiesCollection, SoftBodiesCollection>().ToSingleton();
        bindingProvider.Bind<IHardBodiesCollection, HardBodiesCollection>().ToSingleton();
        bindingProvider.Bind<ISoftBodyFactory, SoftBodyFactory>().ToSingleton();
        bindingProvider.Bind<IHardBodyFactory, HardBodyFactory>().ToSingleton();
        bindingProvider.Bind<IPhysicsWorldUpdater, PhysicsWorldUpdater>().ToSingleton();
        bindingProvider.Bind<IPhysicsWorld, PhysicsWorld>().ToSingleton();
    }
}
