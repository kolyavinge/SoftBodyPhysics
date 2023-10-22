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
        var allMassPoints = _softBodiesCollection.AllMassPoints;
        for (var i = 0; i < allMassPoints.Length; i++)
        {
            var massPoint = allMassPoints[i];
            massPoint.Collision = null;
        }

        var allEdges = _hardBodiesCollection.AllEdges;
        for (var i = 0; i < allEdges.Length; i++)
        {
            var edge = allEdges[i];
            edge.Collisions.Clear();
        }
    }
}
