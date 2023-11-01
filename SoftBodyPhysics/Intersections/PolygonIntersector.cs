using System.Collections.Generic;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Intersections;

internal interface IPolygonIntersector
{
    bool IsPointInPolygon(IEnumerable<ISegment> polygonPoints, Borders borders, Vector point);
}

internal class PolygonIntersector : IPolygonIntersector
{
    private readonly ISegmentIntersector _segmentIntersector;

    public PolygonIntersector(ISegmentIntersector segmentIntersector)
    {
        _segmentIntersector = segmentIntersector;
    }

    public bool IsPointInPolygon(IEnumerable<ISegment> polygonPoints, Borders borders, Vector point)
    {
        if (!IsPointIn(borders, point, 1.0f)) return false;
        var pointTo = new Vector(point.x, 2.0f * borders.MaxY);
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

    private bool IsPointIn(Borders borders, Vector point, float delta)
    {
        return
            borders.MinX - delta <= point.x && point.x <= borders.MaxX + delta &&
            borders.MinY - delta <= point.y && point.y <= borders.MaxY + delta;
    }
}
