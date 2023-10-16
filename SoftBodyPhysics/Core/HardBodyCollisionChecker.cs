using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Intersections;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IHardBodyCollisionChecker
{
    void CheckCollisions(SoftBody softBody);
}

internal class HardBodyCollisionChecker : IHardBodyCollisionChecker
{
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly ISegmentIntersectDetector _segmentIntersectDetector;
    private readonly INormalCalculator _normalCalculator;
    private readonly IPhysicsUnits _physicsUnits;

    public HardBodyCollisionChecker(
        IHardBodiesCollection hardBodiesCollection,
        ISegmentIntersectDetector segmentIntersectDetector,
        INormalCalculator normalCalculator,
        IPhysicsUnits physicsUnits)
    {
        _hardBodiesCollection = hardBodiesCollection;
        _segmentIntersectDetector = segmentIntersectDetector;
        _normalCalculator = normalCalculator;
        _physicsUnits = physicsUnits;
    }

    public void CheckCollisions(SoftBody softBody)
    {
        foreach (var massPoint in softBody.MassPoints)
        {
            foreach (var hardBody in _hardBodiesCollection.HardBodies)
            {
                if (hardBody.Borders.MinX - 1.0f < massPoint.Position.X && massPoint.Position.X < hardBody.Borders.MaxX + 1.0f &&
                    hardBody.Borders.MinY - 1.0f < massPoint.Position.Y && massPoint.Position.Y < hardBody.Borders.MaxY + 1.0f)
                {
                    if (CheckMassPointAndEdgeCollision(massPoint, hardBody.Edges)) break;
                }
            }
        }
    }

    private bool CheckMassPointAndEdgeCollision(MassPoint massPoint, Edge[] edges)
    {
        foreach (var edge in edges)
        {
            if (!_segmentIntersectDetector.Intersected(edge.From, edge.To, massPoint.Position)) continue;

            var normal = _normalCalculator.GetNormal(edge.From, edge.To);
            edge.State = CollisionState.Collision;
            massPoint.State = CollisionState.Collision;
            massPoint.Position = massPoint.PrevPosition;
            massPoint.Velocity -= 2.0f * (massPoint.Velocity * normal) * normal; // reflected vector
            massPoint.Velocity *= 1.0f - _physicsUnits.Friction;

            return true;
        }

        return false;
    }
}
