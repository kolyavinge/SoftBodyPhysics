using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

internal interface ISoftBodyIntersector
{
    (Spring?, Vector?) GetIntersectPoint(SoftBody softBody, Vector point);
}

internal class SoftBodyIntersector : ISoftBodyIntersector
{
    private readonly ISegmentIntersector _segmentIntersector;

    public SoftBodyIntersector(ISegmentIntersector segmentIntersector)
    {
        _segmentIntersector = segmentIntersector;
    }

    public (Spring?, Vector?) GetIntersectPoint(SoftBody softBody, Vector point)
    {
        var pointTo = new Vector(point.X, 10000);
        var intersectPoints = new List<(Spring, Vector)>();

        foreach (var spring in softBody.Springs.Where(x => x.IsEdge))
        {
            var p = _segmentIntersector.GetIntersectPoint(spring.PointA.Position, spring.PointB.Position, point, pointTo);
            if (p is not null)
            {
                intersectPoints.Add((spring, p.Value));
            }
        }

        if ((intersectPoints.Count % 2) != 0)
        {
            var nearest = intersectPoints
                .Select(x => new { Spring = x.Item1, Point = x.Item2, Distance = (point - x.Item2).Length })
                .OrderBy(x => x.Distance)
                .First();

            return (nearest.Spring, nearest.Point);
        }
        else
        {
            return (null, null);
        }
    }
}
