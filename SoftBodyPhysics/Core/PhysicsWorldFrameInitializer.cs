namespace SoftBodyPhysics.Core;

internal interface IPhysicsWorldFrameInitializer
{
    void Init();
}

internal class PhysicsWorldFrameInitializer : IPhysicsWorldFrameInitializer
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;

    public PhysicsWorldFrameInitializer(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
    }

    public void Init()
    {
        foreach (var massPoint in _softBodiesCollection.AllMassPoints)
        {
            massPoint.Collision = null;
        }
        foreach (var spring in _hardBodiesCollection.AllEdges)
        {
            spring.Collisions.Clear();
        }
    }
}
