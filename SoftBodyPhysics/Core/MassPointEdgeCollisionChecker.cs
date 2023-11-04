using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Intersections;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IMassPointEdgeCollisionChecker
{
    bool CheckMassPointAndEdgeCollision(MassPoint massPoint, Edge[] edges);
}

internal class MassPointEdgeCollisionChecker : IMassPointEdgeCollisionChecker
{
    private readonly ISegmentIntersector _segmentIntersector;
    private readonly IVectorCalculator _vectorCalculator;
    private readonly IPhysicsUnits _physicsUnits;
    private readonly Vector _normal;

    public MassPointEdgeCollisionChecker(
        ISegmentIntersector segmentIntersector,
        IVectorCalculator vectorCalculator,
        IPhysicsUnits physicsUnits)
    {
        _segmentIntersector = segmentIntersector;
        _vectorCalculator = vectorCalculator;
        _physicsUnits = physicsUnits;
        _normal = new(0, 0);
    }

    public bool CheckMassPointAndEdgeCollision(MassPoint massPoint, Edge[] edges)
    {
        for (var i = 0; i < edges.Length; i++)
        {
            var edge = edges[i];
            if (!_segmentIntersector.IsIntersected(edge.From, edge.To, massPoint.Position)) continue;

            _vectorCalculator.GetNormalVector(edge.From, edge.To, _normal);

            edge.Collisions.Add(massPoint);

            massPoint.Collision = edge;
            massPoint.Position.x = massPoint.PrevPosition.x;
            massPoint.Position.y = massPoint.PrevPosition.y;

            _vectorCalculator.ReflectVector(massPoint.Velocity, _normal);
            massPoint.Velocity.x *= _physicsUnits.Sliding;
            massPoint.Velocity.y *= _physicsUnits.Sliding;

            return true;
        }

        return false;
    }
}
