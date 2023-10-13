using System.Collections.Generic;
using SoftBodyPhysics.Ancillary;
using SoftBodyPhysics.Factories;

namespace SoftBodyPhysics.Model;

public interface IPhysicsWorld
{
    IReadOnlyCollection<ISoftBody> SoftBodies { get; }

    IReadOnlyCollection<IHardBody> HardBodies { get; }

    IPhysicsUnits Units { get; }

    IBodyEditor MakEditor();

    void Update();
}

internal class PhysicsWorld : IPhysicsWorld
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly IBodyEditorFactory _bodyEditorFactory;
    private readonly IPhysicsWorldUpdater _updater;

    public IReadOnlyCollection<ISoftBody> SoftBodies => _softBodiesCollection.SoftBodies;

    public IReadOnlyCollection<IHardBody> HardBodies => _hardBodiesCollection.HardBodies;

    public IPhysicsUnits Units { get; }

    public PhysicsWorld(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        IBodyEditorFactory bodyEditorFactory,
        IPhysicsWorldUpdater updater,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _bodyEditorFactory = bodyEditorFactory;
        _updater = updater;
        Units = physicsUnits;
    }

    public IBodyEditor MakEditor()
    {
        return _bodyEditorFactory.Make();
    }

    public void Update()
    {
        _updater.Update();
    }
}
