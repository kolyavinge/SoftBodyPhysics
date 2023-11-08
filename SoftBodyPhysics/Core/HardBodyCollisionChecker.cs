using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IHardBodyCollisionChecker
{
    void CheckCollisions(SoftBody softBody);
}

internal class HardBodyCollisionChecker : IHardBodyCollisionChecker
{
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly IMassPointEdgeCollisionChecker _collisionChecker;
    private readonly IBodyCollisionCollection _bodyCollisionCollection;

    public HardBodyCollisionChecker(
        IHardBodiesCollection hardBodiesCollection,
        IMassPointEdgeCollisionChecker collisionChecker,
        IBodyCollisionCollection bodyCollisionCollection)
    {
        _hardBodiesCollection = hardBodiesCollection;
        _collisionChecker = collisionChecker;
        _bodyCollisionCollection = bodyCollisionCollection;
    }

    public void CheckCollisions(SoftBody softBody)
    {
        var hardBodies = _hardBodiesCollection.HardBodies;
        var massPoints = softBody.EdgeMassPoints;

        for (var i = 0; i < massPoints.Length; i++)
        {
            var massPoint = massPoints[i];

            for (var j = 0; j < hardBodies.Length; j++)
            {
                var hardBody = hardBodies[j];

                if (hardBody.Borders.MinX - 1.0f < massPoint.Position.x && massPoint.Position.x < hardBody.Borders.MaxX + 1.0f &&
                    hardBody.Borders.MinY - 1.0f < massPoint.Position.y && massPoint.Position.y < hardBody.Borders.MaxY + 1.0f)
                {
                    if (_collisionChecker.CheckMassPointAndEdgeCollision(massPoint, hardBody.Edges))
                    {
                        _bodyCollisionCollection.SetCollision(softBody, hardBody);
                        break;
                    }
                }
            }
        }
    }
}
