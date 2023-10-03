using System.Collections.Generic;
using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Intersections;

internal interface IPolygonChecker
{
    bool IsPointInPolygon(IEnumerable<ISegment> polygonPoints, Borders borders, Vector point);
}

internal class PolygonChecker : IPolygonChecker
{
    private readonly ISegmentIntersector _segmentIntersector;

    public PolygonChecker(ISegmentIntersector segmentIntersector)
    {
        _segmentIntersector = segmentIntersector;
    }

    public bool IsPointInPolygon(IEnumerable<ISegment> polygonPoints, Borders borders, Vector point)
    {
        if (!borders.IsPointIn(point)) return false;
        var pointTo = new Vector(point.X, borders.MaxY);
        int intersections = 0;
        foreach (var polygonPoint in polygonPoints)
        {
            if (_segmentIntersector.GetIntersectPoint(polygonPoint.PointA.Position, polygonPoint.PointB.Position, point, pointTo) is not null)
            {
                intersections++;
            }
        }

        return intersections % 2 != 0;
    }
}
