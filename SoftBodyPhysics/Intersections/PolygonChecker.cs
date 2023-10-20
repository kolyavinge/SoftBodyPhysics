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
        if (!IsPointIn(borders, point, 10.0)) return false;
        var pointTo = new Vector(point.x, borders.MaxY + 1000.0f);
        int intersections = 0;
        foreach (var polygonPoint in polygonPoints)
        {
            if (_segmentIntersector.GetIntersectPoint(polygonPoint.FromPosition, polygonPoint.ToPosition, point, pointTo) is not null)
            {
                intersections++;
            }
        }

        return intersections % 2 != 0;
    }

    private bool IsPointIn(Borders borders, Vector point, double delta)
    {
        return
            borders.MinX - delta <= point.x && point.x <= borders.MaxX + delta &&
            borders.MinY - delta <= point.y && point.y <= borders.MaxY + delta;
    }
}
