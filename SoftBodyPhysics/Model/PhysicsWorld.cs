using System.Collections.Generic;

namespace SoftBodyPhysics.Model;

public interface IPhysicsWorld
{
    IReadOnlyCollection<ISoftBody> SoftBodies { get; }

    IReadOnlyCollection<IHardBody> HardBodies { get; }

    ISoftBody AddSoftBody();

    IHardBody AddHardBody();

    void Update();
}

internal class PhysicsWorld : IPhysicsWorld
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly IPhysicsWorldUpdater _updater;

    public IReadOnlyCollection<ISoftBody> SoftBodies => _softBodiesCollection.SoftBodies;

    public IReadOnlyCollection<IHardBody> HardBodies => _hardBodiesCollection.HardBodies;

    public PhysicsWorld(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        IPhysicsWorldUpdater updater)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _updater = updater;
    }

    public ISoftBody AddSoftBody()
    {
        var softBody = new SoftBody();
        _softBodiesCollection.AddSoftBody(softBody);

        return softBody;
    }

    public IHardBody AddHardBody()
    {
        var hardBody = new HardBody();
        _hardBodiesCollection.AddHardBody(hardBody);

        return hardBody;
    }

    public void Update()
    {
        _updater.Update();
    }
}
