namespace SoftBodyPhysics.Core;

internal interface ICollisionChecker
{
    void CheckCollisions();
}

internal class CollisionChecker : ICollisionChecker
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly ISoftBodyCollisionChecker _softBodyCollisionChecker;
    private readonly IHardBodyCollisionChecker _hardBodyCollisionChecker;

    public CollisionChecker(
        ISoftBodiesCollection softBodiesCollection,
        ISoftBodyCollisionChecker softBodyCollisionChecker,
        IHardBodyCollisionChecker hardBodyCollisionChecker)
    {
        _softBodiesCollection = softBodiesCollection;
        _softBodyCollisionChecker = softBodyCollisionChecker;
        _hardBodyCollisionChecker = hardBodyCollisionChecker;
    }

    public void CheckCollisions()
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
