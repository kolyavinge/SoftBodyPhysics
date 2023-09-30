using System;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Intersections;

internal interface ISpringIntersector
{
    Vector? GetIntersectPoint(
        Vector springPositionFrom,
        Vector springPositionTo,
        Vector springPrevPositionFrom,
        Vector springPrevPositionTo,
        Vector massPointPosition);
}

internal class SpringIntersector : ISpringIntersector
{
    private readonly IPolygonChecker _polygonChecker;
    private readonly ISegmentIntersector _segmentIntersector;

    public SpringIntersector(
        IPolygonChecker polygonChecker,
        ISegmentIntersector segmentIntersector)
    {
        _polygonChecker = polygonChecker;
        _segmentIntersector = segmentIntersector;
    }

    public Vector? GetIntersectPoint(
        Vector springPositionFrom,
        Vector springPositionTo,
        Vector springPrevPositionFrom,
        Vector springPrevPositionTo,
        Vector massPointPosition)
    {
        var maxY = Vectors.GetMaxY(springPositionFrom, springPositionTo, springPrevPositionFrom, springPrevPositionTo, massPointPosition);

        var polygonPoints = new[]
        {
            (springPositionFrom, springPositionTo),
            (springPrevPositionFrom, springPrevPositionTo),
            (springPrevPositionFrom, springPositionFrom),
            (springPrevPositionTo, springPositionTo),
        };

        if (_polygonChecker.IsPointInPolygon(polygonPoints, massPointPosition, maxY))
        {
            var minY = Math.Min(Math.Min(springPositionFrom.Y, springPositionTo.Y), Math.Min(springPrevPositionFrom.Y, springPrevPositionTo.Y));
            var minX = Math.Min(Math.Min(springPositionFrom.X, springPositionTo.X), Math.Min(springPrevPositionFrom.X, springPrevPositionTo.X));
            var maxX = Math.Max(Math.Max(springPositionFrom.X, springPositionTo.X), Math.Max(springPrevPositionFrom.X, springPrevPositionTo.X));

            var intersectPoint =
                _segmentIntersector.GetIntersectPoint(springPositionFrom, springPositionTo, massPointPosition, new(massPointPosition.X, maxY)) ??
                _segmentIntersector.GetIntersectPoint(springPositionFrom, springPositionTo, massPointPosition, new(massPointPosition.X, minY)) ??
                _segmentIntersector.GetIntersectPoint(springPositionFrom, springPositionTo, massPointPosition, new(minX, massPointPosition.Y)) ??
                _segmentIntersector.GetIntersectPoint(springPositionFrom, springPositionTo, massPointPosition, new(maxX, massPointPosition.Y));

            return intersectPoint;
        }
        else
        {
            return null;
        }
    }
}
