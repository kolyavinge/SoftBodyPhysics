﻿namespace SoftBodyPhysics.Core;

internal interface IPhysicsWorldUpdater
{
    void UpdateFrame();
}

internal class PhysicsWorldUpdater : IPhysicsWorldUpdater
{
    private readonly ISoftBodyActivator _softBodyActivator;
    private readonly IPhysicsWorldFrameInitializer _frameInitializer;
    private readonly IGravityForceCalculator _gravityForceCalculator;
    private readonly ISpringForceCalculator _springForceCalculator;
    private readonly IVelocityCalculator _velocityCalculator;
    private readonly ITimeStepCalculator _timeStepCalculator;
    private readonly ICollisionChecker _collisionChecker;
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IPhysicsUnits _physicsUnits;

    public PhysicsWorldUpdater(
        ISoftBodyActivator softBodyActivator,
        IPhysicsWorldFrameInitializer frameInitializer,
        IGravityForceCalculator gravityForceCalculator,
        ISpringForceCalculator springForceCalculator,
        IVelocityCalculator velocityCalculator,
        ITimeStepCalculator timeStepCalculator,
        ICollisionChecker collisionChecker,
        ISoftBodiesCollection softBodiesCollection,
        IPhysicsUnits physicsUnits)
    {
        _softBodyActivator = softBodyActivator;
        _frameInitializer = frameInitializer;
        _gravityForceCalculator = gravityForceCalculator;
        _springForceCalculator = springForceCalculator;
        _velocityCalculator = velocityCalculator;
        _timeStepCalculator = timeStepCalculator;
        _collisionChecker = collisionChecker;
        _softBodiesCollection = softBodiesCollection;
        _physicsUnits = physicsUnits;
    }

    public void UpdateFrame()
    {
        _frameInitializer.Init();
        float timeStep;
        for (var time = 0.0f; time < _physicsUnits.Time; time += timeStep)
        {
            timeStep = _physicsUnits.Time;
            _gravityForceCalculator.InitGravityForce();
            _springForceCalculator.ApplySpringForce();
            _velocityCalculator.CalculatePositionStep(timeStep);
            timeStep = _timeStepCalculator.GetTimeStep(timeStep);
            _velocityCalculator.ApplyVelocity(timeStep);
            _collisionChecker.CheckCollisions();
        }
        _softBodyActivator.Activate();
        _softBodiesCollection.UpdateActivatedSoftBodies();
    }
}
