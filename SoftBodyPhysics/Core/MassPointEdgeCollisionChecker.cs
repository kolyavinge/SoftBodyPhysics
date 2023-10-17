using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Intersections;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IMassPointEdgeCollisionChecker
{
    bool CheckMassPointAndEdgeCollision(MassPoint massPoint, Edge[] edges);
}

internal class MassPointEdgeCollisionChecker : IMassPointEdgeCollisionChecker
{
    private readonly ISegmentIntersectDetector _segmentIntersectDetector;
    private readonly IVectorCalculator _vectorCalculator;
    private readonly IPhysicsUnits _physicsUnits;

    public MassPointEdgeCollisionChecker(
        ISegmentIntersectDetector segmentIntersectDetector,
        IVectorCalculator vectorCalculator,
        IPhysicsUnits physicsUnits)
    {
        _segmentIntersectDetector = segmentIntersectDetector;
        _vectorCalculator = vectorCalculator;
        _physicsUnits = physicsUnits;
    }

    public bool CheckMassPointAndEdgeCollision(MassPoint massPoint, Edge[] edges)
    {
        foreach (var edge in edges)
        {
            if (!_segmentIntersectDetector.Intersected(edge.From, edge.To, massPoint.Position)) continue;

            var normal = _vectorCalculator.GetNormalVector(edge.From, edge.To);
            edge.State = CollisionState.Collision;
            massPoint.State = CollisionState.Collision;
            massPoint.Position = massPoint.PrevPosition;
            massPoint.Velocity = _vectorCalculator.GetReflectedVector(massPoint.Velocity, normal);
            massPoint.Velocity *= 1.0f - _physicsUnits.Friction;

            return true;
        }

        return false;
    }
}
