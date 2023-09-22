using System.Collections.Generic;
using SoftBodyPhysics.Factories;

namespace SoftBodyPhysics.Model;

public interface IPhysicsWorld
{
    IReadOnlyCollection<ISoftBody> SoftBodies { get; }

    IReadOnlyCollection<IHardBody> HardBodies { get; }

    IPhysicsUnits Units { get; }

    ISoftBody AddSoftBody();

    IHardBody AddHardBody();

    void Update();
}

internal class PhysicsWorld : IPhysicsWorld
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly ISoftBodyFactory _softBodyFactory;
    private readonly IHardBodyFactory _hardBodyFactory;
    private readonly IPhysicsWorldUpdater _updater;

    public IReadOnlyCollection<ISoftBody> SoftBodies => _softBodiesCollection.SoftBodies;

    public IReadOnlyCollection<IHardBody> HardBodies => _hardBodiesCollection.HardBodies;

    public IPhysicsUnits Units { get; }

    public PhysicsWorld(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        ISoftBodyFactory softBodyFactory,
        IHardBodyFactory hardBodyFactory,
        IPhysicsWorldUpdater updater,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _softBodyFactory = softBodyFactory;
        _hardBodyFactory = hardBodyFactory;
        _updater = updater;
        Units = physicsUnits;
    }

    public ISoftBody AddSoftBody()
    {
        var softBody = _softBodyFactory.Make();
        _softBodiesCollection.AddSoftBody(softBody);

        return softBody;
    }

    public IHardBody AddHardBody()
    {
        var hardBody = _hardBodyFactory.Make();
        _hardBodiesCollection.AddHardBody(hardBody);

        return hardBody;
    }

    public void Update()
    {
        _updater.Update();
    }
}
