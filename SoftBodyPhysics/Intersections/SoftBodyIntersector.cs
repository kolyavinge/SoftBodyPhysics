using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Intersections;

internal class IntersectPoint
{
    public readonly Spring Spring;
    public readonly Vector Point;

    public IntersectPoint(Spring spring, Vector point)
    {
        Spring = spring;
        Point = point;
    }
}

internal interface ISoftBodyIntersector
{
    IntersectPoint? GetIntersectPoint(SoftBody softBody, Vector point);
}

internal class SoftBodyIntersector : ISoftBodyIntersector
{
    private readonly IPolygonChecker _polygonChecker;
    private readonly ISegmentIntersector _segmentIntersector;

    public SoftBodyIntersector(
        IPolygonChecker polygonChecker,
        ISegmentIntersector segmentIntersector)
    {
        _polygonChecker = polygonChecker;
        _segmentIntersector = segmentIntersector;
    }

    public IntersectPoint? GetIntersectPoint(SoftBody softBody, Vector point)
    {
        var edges = softBody.Springs.Where(x => x.IsEdge).ToList();
        var polygonPoints = edges.Select(x => (x.PointA.Position, x.PointB.Position)).ToList();
        var vectors = polygonPoints.Select(x => x.Item1).Union(polygonPoints.Select(x => x.Item2)).Union(new[] { point }).ToArray();
        var maxY = Vectors.GetMaxY(vectors);

        if (!_polygonChecker.IsPointInPolygon(polygonPoints, point, maxY)) return null;

        var minX = Vectors.GetMinX(vectors);
        var maxX = Vectors.GetMaxX(vectors);
        var minY = Vectors.GetMinY(vectors);

        var intersectPoints = new List<IntersectPoint>();
        foreach (var edge in edges)
        {
            var p = _segmentIntersector.GetIntersectPoint(edge.PointA.Position, edge.PointB.Position, point, new(minX, point.Y));
            if (p is not null) intersectPoints.Add(new(edge, p.Value));

            p = _segmentIntersector.GetIntersectPoint(edge.PointA.Position, edge.PointB.Position, point, new(maxX, point.Y));
            if (p is not null) intersectPoints.Add(new(edge, p.Value));

            p = _segmentIntersector.GetIntersectPoint(edge.PointA.Position, edge.PointB.Position, point, new(point.X, minY));
            if (p is not null) intersectPoints.Add(new(edge, p.Value));

            p = _segmentIntersector.GetIntersectPoint(edge.PointA.Position, edge.PointB.Position, point, new(point.X, maxY));
            if (p is not null) intersectPoints.Add(new(edge, p.Value));
        }

        var result = intersectPoints.First();
        var distance = (result.Point - point).Length;
        foreach (var intersectPoint in intersectPoints.Skip(1))
        {
            var currentDistance = (intersectPoint.Point - point).Length;
            if (currentDistance < distance)
            {
                result = intersectPoint;
                distance = currentDistance;
            }
        }

        return result;
    }
}
