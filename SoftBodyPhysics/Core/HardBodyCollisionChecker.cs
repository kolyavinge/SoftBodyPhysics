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

    public HardBodyCollisionChecker(
        IHardBodiesCollection hardBodiesCollection,
        IMassPointEdgeCollisionChecker collisionChecker)
    {
        _hardBodiesCollection = hardBodiesCollection;
        _collisionChecker = collisionChecker;
    }

    public void CheckCollisions(SoftBody softBody)
    {
        foreach (var massPoint in softBody.MassPoints)
        {
            foreach (var hardBody in _hardBodiesCollection.HardBodies)
            {
                if (hardBody.Borders.MinX - 1.0f < massPoint.Position.x && massPoint.Position.x < hardBody.Borders.MaxX + 1.0f &&
                    hardBody.Borders.MinY - 1.0f < massPoint.Position.y && massPoint.Position.y < hardBody.Borders.MaxY + 1.0f)
                {
                    if (_collisionChecker.CheckMassPointAndEdgeCollision(massPoint, hardBody.Edges)) break;
                }
            }
        }
    }
}
