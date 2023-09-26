using System;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

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
    private readonly ISegmentIntersector _segmentIntersector;

    public SpringIntersector(
        ISegmentIntersector segmentIntersector)
    {
        _segmentIntersector = segmentIntersector;
    }

    public Vector? GetIntersectPoint(
        Vector springPositionFrom,
        Vector springPositionTo,
        Vector springPrevPositionFrom,
        Vector springPrevPositionTo,
        Vector massPointPosition)
    {
        var maxY = Math.Max(Math.Max(Math.Max(springPositionFrom.Y, springPositionTo.Y), Math.Max(springPrevPositionFrom.Y, springPrevPositionTo.Y)), massPointPosition.Y);
        var massPointPositionTo = new Vector(massPointPosition.X, maxY);

        int intersections = 0;

        if (_segmentIntersector.GetIntersectPoint(springPositionFrom, springPositionTo, massPointPosition, massPointPositionTo) is not null) intersections++;
        if (_segmentIntersector.GetIntersectPoint(springPrevPositionFrom, springPrevPositionTo, massPointPosition, massPointPositionTo) is not null) intersections++;
        if (_segmentIntersector.GetIntersectPoint(springPrevPositionFrom, springPositionFrom, massPointPosition, massPointPositionTo) is not null) intersections++;
        if (_segmentIntersector.GetIntersectPoint(springPrevPositionTo, springPositionTo, massPointPosition, massPointPositionTo) is not null) intersections++;

        if ((intersections % 2) != 0)
        {
            var minY = Math.Min(Math.Min(springPositionFrom.Y, springPositionTo.Y), Math.Min(springPrevPositionFrom.Y, springPrevPositionTo.Y));
            var minX = Math.Min(Math.Min(springPositionFrom.X, springPositionTo.X), Math.Min(springPrevPositionFrom.X, springPrevPositionTo.X));
            var maxX = Math.Max(Math.Max(springPositionFrom.X, springPositionTo.X), Math.Max(springPrevPositionFrom.X, springPrevPositionTo.X));

            var intersectPoint =
                _segmentIntersector.GetIntersectPoint(springPositionFrom, springPositionTo, massPointPosition, massPointPositionTo) ??
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
