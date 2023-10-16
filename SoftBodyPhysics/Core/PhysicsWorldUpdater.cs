using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Core;

internal interface IPhysicsWorldUpdater
{
    void Update();
}

internal class PhysicsWorldUpdater : IPhysicsWorldUpdater
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly ISoftBodyBordersUpdater _softBodyBordersUpdater;
    private readonly ITimeStepCalculator _timeStepCalculator;
    private readonly ISoftBodyCollisionChecker _softBodyCollisionChecker;
    private readonly IHardBodyCollisionChecker _hardBodyCollisionChecker;
    private readonly IPhysicsUnits _physicsUnits;

    public PhysicsWorldUpdater(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        ISoftBodyBordersUpdater softBodyBordersUpdater,
        ITimeStepCalculator timeStepCalculator,
        ISoftBodyCollisionChecker softBodyCollisionChecker,
        IHardBodyCollisionChecker hardBodyCollisionChecker,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _softBodyBordersUpdater = softBodyBordersUpdater;
        _timeStepCalculator = timeStepCalculator;
        _softBodyCollisionChecker = softBodyCollisionChecker;
        _hardBodyCollisionChecker = hardBodyCollisionChecker;
        _physicsUnits = physicsUnits;
    }

    public void Update()
    {
        Init();
        var timeStep = _physicsUnits.Time;
        for (var time = 0.0f; time < _physicsUnits.Time; time += timeStep)
        {
            InitGravityForce();
            ApplySpringForce();
            ApplyVelocity(timeStep);
            timeStep = _timeStepCalculator.GetTimeStepAndCorrectPositionSteps(timeStep);
            CheckCollisions();
            _softBodyBordersUpdater.UpdateBorders();
        }
    }

    private void Init()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.ResetState();
        }
        foreach (var spring in _hardBodiesCollection.AllEdges)
        {
            spring.ResetState();
        }
    }

    private void InitGravityForce()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Force = new Vector(0, -_physicsUnits.GravityAcceleration * massPoint.Mass);
        }
    }

    private void ApplySpringForce()
    {
        foreach (var spring in _softBodiesCollection.AllSprings)
        {
            var a = spring.PointA;
            var b = spring.PointB;
            var fs = spring.Stiffness * spring.DeformLength;
            var diffBA = (b.Position - a.Position).Normalized;
            var fd = _physicsUnits.SpringDamper * (diffBA * (b.Velocity - a.Velocity)); // (B.Position - A.Position).Normalized * (B.Velocity - A.Velocity)
            var f = fs + fd;
            var df = f * diffBA;
            a.Force += df; // (B.Position - A.Position).Normalized
            b.Force -= df; // (A.Position - B.Position).Normalized
        }
    }

    private void ApplyVelocity(float timeStep)
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Velocity += massPoint.Force * (timeStep / massPoint.Mass);
            massPoint.PositionStep = massPoint.Velocity * timeStep;
        }
    }

    private void CheckCollisions()
    {
        foreach (var softBody in _softBodiesCollection.SoftBodies)
        {
            foreach (var massPoint in softBody.MassPoints)
            {
                massPoint.Position += massPoint.PositionStep;
            }
            _softBodyCollisionChecker.CheckCollisions(softBody);
            _hardBodyCollisionChecker.CheckCollisions(softBody);
            foreach (var massPoint in softBody.MassPoints)
            {
                massPoint.SavePosition();
            }
        }
    }
}
