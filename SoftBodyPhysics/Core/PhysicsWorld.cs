using System.Collections.Generic;
using System.Linq;
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

    ISoftBodyEditor MakeSoftBodyEditor();

    IHardBodyEditor MakeHardBodyEditor();

    void Update();

    ISoftBody? GetSoftBodyByMassPoint(IMassPoint massPoint);

    IEnumerable<ISoftBody> GetCollidedSoftBodies(IBody body);

    IEnumerable<IHardBody> GetCollidedHardBodies(ISoftBody softBody);

    bool IsCollidedToAnySoftBody(IBody body);

    bool IsCollidedToAnyHardBody(ISoftBody softBody);

    IEnumerable<ISoftBody> GetSoftBodyByPosition(Vector point);
}

internal class PhysicsWorld : IPhysicsWorld
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly IBodyEditorFactory _bodyEditorFactory;
    private readonly IPhysicsWorldUpdater _updater;
    private readonly ISoftBodyIntersector _softBodyIntersector;
    private readonly IBodyCollisionCollection _bodyCollisionCollection;

    public IReadOnlyCollection<ISoftBody> SoftBodies => _softBodiesCollection.SoftBodies;

    public IReadOnlyCollection<IHardBody> HardBodies => _hardBodiesCollection.HardBodies;

    public IPhysicsUnits Units { get; }

    public PhysicsWorld(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        IBodyEditorFactory bodyEditorFactory,
        IPhysicsWorldUpdater updater,
        ISoftBodyIntersector softBodyIntersector,
        IBodyCollisionCollection bodyCollisionCollection,
        IPhysicsUnits physicsUnits)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _bodyEditorFactory = bodyEditorFactory;
        _updater = updater;
        _softBodyIntersector = softBodyIntersector;
        _bodyCollisionCollection = bodyCollisionCollection;
        Units = physicsUnits;
    }

    public ISoftBodyEditor MakeSoftBodyEditor()
    {
        return _bodyEditorFactory.MakeSoftBodyEditor();
    }

    public IHardBodyEditor MakeHardBodyEditor()
    {
        return _bodyEditorFactory.MakeHardBodyEditor();
    }

    public void Update()
    {
        _updater.UpdateFrame();
    }

    public ISoftBody? GetSoftBodyByMassPoint(IMassPoint massPoint)
    {
        return _softBodiesCollection.SoftBodies.FirstOrDefault(x => x.MassPoints.Contains(massPoint));
    }

    public IEnumerable<ISoftBody> GetCollidedSoftBodies(IBody body)
    {
        foreach (var index in _bodyCollisionCollection.GetCollidedSoftBodyIndexes(body))
        {
            yield return _softBodiesCollection.SoftBodies[index];
        }
    }

    public IEnumerable<IHardBody> GetCollidedHardBodies(ISoftBody softBody)
    {
        foreach (var index in _bodyCollisionCollection.GetCollidedHardBodyIndexes(softBody))
        {
            yield return _hardBodiesCollection.HardBodies[index];
        }
    }

    public bool IsCollidedToAnySoftBody(IBody body)
    {
        return _bodyCollisionCollection.IsCollidedToAnySoftBody(body);
    }

    public bool IsCollidedToAnyHardBody(ISoftBody softBody)
    {
        return _bodyCollisionCollection.IsCollidedToAnyHardBody(softBody);
    }

    public IEnumerable<ISoftBody> GetSoftBodyByPosition(Vector point)
    {
        return _softBodyIntersector.GetSoftBodyByPoint(point);
    }
}
