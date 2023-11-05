namespace SoftBodyPhysics.Core;

internal interface ICollisionChecker
{
    void CheckCollisions();
}

internal class CollisionChecker : ICollisionChecker
{
    private readonly IBodyBordersUpdater _bodyBordersUpdater;
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly ISoftBodyCollisionChecker _softBodyCollisionChecker;
    private readonly IHardBodyCollisionChecker _hardBodyCollisionChecker;

    public CollisionChecker(
        IBodyBordersUpdater bodyBordersUpdater,
        ISoftBodiesCollection softBodiesCollection,
        ISoftBodyCollisionChecker softBodyCollisionChecker,
        IHardBodyCollisionChecker hardBodyCollisionChecker)
    {
        _bodyBordersUpdater = bodyBordersUpdater;
        _softBodiesCollection = softBodiesCollection;
        _softBodyCollisionChecker = softBodyCollisionChecker;
        _hardBodyCollisionChecker = hardBodyCollisionChecker;
    }

    public void CheckCollisions()
    {
        ApplyPositionStepCheckHardBodyCollisionsUpdateBorders();
        CheckCollisions1();
        SavePositions();
    }

    private void ApplyPositionStepCheckHardBodyCollisionsUpdateBorders()
    {
        var softBodies = _softBodiesCollection.ActivatedSoftBodies;
        var count = _softBodiesCollection.ActivatedSoftBodiesCount;
        for (var i = 0; i < count; i++)
        {
            var softBody = softBodies[i];
            var massPoints = softBody.MassPoints;
            for (var j = 0; j < massPoints.Length; j++)
            {
                var massPoint = massPoints[j];
                massPoint.Position.x += massPoint.PositionStep.x;
                massPoint.Position.y += massPoint.PositionStep.y;
            }
            _hardBodyCollisionChecker.CheckCollisions(softBody);
            _bodyBordersUpdater.UpdateBorders(softBody);
        }
    }

    private void CheckCollisions1()
    {
        var softBodies = _softBodiesCollection.SoftBodies;
        for (var i = 0; i < softBodies.Length - 1; i++)
        {
            var softBody1 = softBodies[i];
            for (int j = i + 1; j < softBodies.Length; j++)
            {
                var softBody2 = softBodies[j];
                if (!softBody1.IsActive && !softBody2.IsActive) continue;
                _softBodyCollisionChecker.CheckCollisions(softBody1, softBody2);
            }
        }
    }

    private void SavePositions()
    {
        var softBodies = _softBodiesCollection.ActivatedSoftBodies;
        var count = _softBodiesCollection.ActivatedSoftBodiesCount;
        for (var i = 0; i < count; i++)
        {
            var softBody = softBodies[i];
            var massPoints = softBody.MassPoints;
            for (var j = 0; j < massPoints.Length; j++)
            {
                var massPoint = massPoints[j];
                massPoint.PrevPosition.x = massPoint.Position.x;
                massPoint.PrevPosition.y = massPoint.Position.y;
            }
        }
    }
}
