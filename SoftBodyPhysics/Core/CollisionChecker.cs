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
        var softBodies = _softBodiesCollection.SoftBodies;
        for (var i = 0; i < softBodies.Length; i++)
        {
            var softBody = softBodies[i];

            var massPoints = softBody.MassPoints;
            for (var j = 0; j < massPoints.Length; j++)
            {
                var massPoint = massPoints[j];
                massPoint.Position.x += massPoint.PositionStep.x;
                massPoint.Position.y += massPoint.PositionStep.y;
            }

            _softBodyCollisionChecker.CheckCollisions(softBody);
            _hardBodyCollisionChecker.CheckCollisions(softBody);

            for (var j = 0; j < massPoints.Length; j++)
            {
                var massPoint = massPoints[j];
                massPoint.PrevPosition.x = massPoint.Position.x;
                massPoint.PrevPosition.y = massPoint.Position.y;
            }
        }
    }
}
