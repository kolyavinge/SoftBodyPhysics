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
        for (var i = 0; i < edges.Length; i++)
        {
            var edge = edges[i];
            if (!_segmentIntersectDetector.Intersected(edge.From, edge.To, massPoint.Position)) continue;

            var normal = _vectorCalculator.GetNormalVector(edge.From, edge.To);

            edge.Collisions.Add(massPoint);

            massPoint.Collision = edge;
            massPoint.Position.x = massPoint.PrevPosition.x;
            massPoint.Position.y = massPoint.PrevPosition.y;

            _vectorCalculator.ReflectVector(massPoint.Velocity, normal);
            massPoint.Velocity.x *= _physicsUnits.Sliding;
            massPoint.Velocity.y *= _physicsUnits.Sliding;

            return true;
        }

        return false;
    }
}
