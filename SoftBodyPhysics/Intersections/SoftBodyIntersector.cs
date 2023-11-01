using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Intersections;

internal class IntersectResult
{
    public readonly Spring Spring;
    public readonly Vector Point;

    public IntersectResult(Spring spring, Vector point)
    {
        Spring = spring;
        Point = point;
    }
}

internal interface ISoftBodyIntersector
{
    IntersectResult? GetIntersectPoint(SoftBody softBody, Vector point);
}

internal class SoftBodyIntersector : ISoftBodyIntersector
{
    private readonly IPolygonIntersector _polygonIntersector;
    private readonly ISegmentIntersector _segmentIntersector;

    public SoftBodyIntersector(
        IPolygonIntersector polygonIntersector,
        ISegmentIntersector segmentIntersector)
    {
        _polygonIntersector = polygonIntersector;
        _segmentIntersector = segmentIntersector;
    }

    public IntersectResult? GetIntersectPoint(SoftBody softBody, Vector point)
    {
        if (softBody.Borders is null) return null;
        if (!_polygonIntersector.IsPointInPolygon(softBody.Edges, softBody.Borders, point)) return null;

        var pointToList = new Vector[]
        {
            new(softBody.Borders.MinX - 1.0f, point.y),
            new(softBody.Borders.MaxX - 1.0f, point.y),
            new(point.x, softBody.Borders.MinY + 1.0f),
            new(point.x, softBody.Borders.MaxY + 1.0f)
        };

        IntersectResult? result = null;

        var minDistance = double.MaxValue;

        foreach (var edge in softBody.Edges)
        {
            foreach (var pointTo in pointToList)
            {
                var intersectPoint = _segmentIntersector.GetIntersectPoint(edge.PointA.Position, edge.PointB.Position, point, pointTo);
                if (intersectPoint is not null)
                {
                    var distance = Vector.GetDistanceBetween(intersectPoint, point);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        result = new(edge, intersectPoint);
                    }
                }
            }
        }

        return result;
    }
}
