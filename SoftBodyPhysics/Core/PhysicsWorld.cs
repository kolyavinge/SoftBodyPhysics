using System.Collections.Generic;
using SoftBodyPhysics.Ancillary;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Intersections;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

public interface IPhysicsWorld
{
    IReadOnlyCollection<ISoftBody> SoftBodies { get; }

    IReadOnlyCollection<IHardBody> HardBodies { get; }

    IPhysicsUnits Units { get; }

    IBodyEditor MakEditor();

    void Update();

    IEnumerable<ISoftBody> GetSoftBodyByPosition(Vector point);
}

internal class PhysicsWorld : IPhysicsWorld
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly IBodyEditorFactory _bodyEditorFactory;
    private readonly IPhysicsWorldUpdater _updater;
    private readonly ISoftBodyIntersector _softBodyIntersector;

    public IReadOnlyCollection<ISoftBody> SoftBodies => _softBodiesCollection.SoftBodies;

    public IReadOnlyCollection<IHardBody> HardBodies => _hardBodiesCollection.HardBodies;

    public IPhysicsUnits Units { get; }

    public PhysicsWorld(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        IBodyEditorFactory bodyEditorFactory,
        IPhysicsWorldUpdater updater,
        ISoftBodyIntersector softBodyIntersector,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _bodyEditorFactory = bodyEditorFactory;
        _updater = updater;
        _softBodyIntersector = softBodyIntersector;
        Units = physicsUnits;
    }

    public IBodyEditor MakEditor()
    {
        return _bodyEditorFactory.Make();
    }

    public void Update()
    {
        _updater.UpdateFrame();
    }

    public IEnumerable<ISoftBody> GetSoftBodyByPosition(Vector point)
    {
        return _softBodyIntersector.GetSoftBodyByPoint(point);
    }
}
