using System.Collections.Generic;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Intersections;

internal interface IPolygonChecker
{
    bool IsPointInPolygon(IEnumerable<(Vector from, Vector to)> polygonPoints, Vector point, double maxY);
}

internal class PolygonChecker : IPolygonChecker
{
    private readonly ISegmentIntersector _segmentIntersector;

    public PolygonChecker(ISegmentIntersector segmentIntersector)
    {
        _segmentIntersector = segmentIntersector;
    }

    public bool IsPointInPolygon(IEnumerable<(Vector from, Vector to)> polygonPoints, Vector point, double maxY)
    {
        var pointTo = new Vector(point.X, maxY);
        int intersections = 0;
        foreach (var (from, to) in polygonPoints)
        {
            if (_segmentIntersector.GetIntersectPoint(from, to, point, pointTo) is not null)
            {
                intersections++;
            }
        }

        return intersections % 2 != 0;
    }
}
