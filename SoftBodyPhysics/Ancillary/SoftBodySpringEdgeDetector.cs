using System.Collections.Generic;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Intersections;
using SoftBodyPhysics.Model;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Ancillary;

internal interface ISoftBodySpringEdgeDetector
{
    void DetectEdges(IReadOnlyCollection<SoftBody> softBodies);
    void DetectEdges(SoftBody softBody);
}

internal class SoftBodySpringEdgeDetector : ISoftBodySpringEdgeDetector
{
    private const float _step = 0.1f;
    private readonly ISegmentIntersector _segmentIntersector;
    private readonly IBordersUpdater _bordersUpdater;
    private readonly Vector _intersectPoint;

    public SoftBodySpringEdgeDetector(
        ISegmentIntersector segmentIntersector,
        IBordersUpdater bordersUpdater)
    {
        _segmentIntersector = segmentIntersector;
        _bordersUpdater = bordersUpdater;
        _intersectPoint = new Vector(0, 0);
    }

    public void DetectEdges(IReadOnlyCollection<SoftBody> softBodies)
    {
        softBodies.Each(DetectEdges);
    }

    public void DetectEdges(SoftBody softBody)
    {
        _bordersUpdater.UpdateBorders(softBody.Borders, softBody.Springs);
        softBody.Springs.Each(s => s.IsEdge = false);
        DetectByVertical(softBody.Springs, softBody.Borders);
        DetectByHorizontal(softBody.Springs, softBody.Borders);
        softBody.UpdateEdges();
    }

    private void DetectByVertical(IEnumerable<Spring> springs, Borders borders)
    {
        var lineFrom = new Vector(0, 0);
        var lineTo = new Vector(0, 0);
        for (var x = borders.MinX; x <= borders.MaxX; x += _step)
        {
            lineFrom.x = x;
            lineFrom.y = borders.MinY;
            lineTo.x = x;
            lineTo.y = borders.MaxY;
            var minY = borders.MaxY;
            var maxY = borders.MinY;
            Spring? minSpring = null, maxSpring = null;
            foreach (var spring in springs)
            {
                if (_segmentIntersector.GetIntersectPoint(spring.PointA.Position, spring.PointB.Position, lineFrom, lineTo, _intersectPoint))
                {
                    if (_intersectPoint.y < minY)
                    {
                        minY = _intersectPoint.y;
                        minSpring = spring;
                    }
                    if (_intersectPoint.y > maxY)
                    {
                        maxY = _intersectPoint.y;
                        maxSpring = spring;
                    }
                }
            }
            if (minSpring is not null && maxSpring is not null)
            {
                minSpring.IsEdge = true;
                maxSpring.IsEdge = true;
            }
        }
    }

    private void DetectByHorizontal(IEnumerable<Spring> springs, Borders borders)
    {
        var lineFrom = new Vector(0, 0);
        var lineTo = new Vector(0, 0);
        for (var y = borders.MinY; y <= borders.MaxY; y += _step)
        {
            lineFrom.x = borders.MinX;
            lineFrom.y = y;
            lineTo.x = borders.MaxX;
            lineTo.y = y;
            var minX = borders.MaxX;
            var maxX = borders.MinX;
            Spring? minSpring = null, maxSpring = null;
            foreach (var spring in springs)
            {
                if (_segmentIntersector.GetIntersectPoint(spring.PointA.Position, spring.PointB.Position, lineFrom, lineTo, _intersectPoint))
                {
                    if (_intersectPoint.x < minX)
                    {
                        minX = _intersectPoint.x;
                        minSpring = spring;
                    }
                    if (_intersectPoint.x > maxX)
                    {
                        maxX = _intersectPoint.x;
                        maxSpring = spring;
                    }
                }
            }
            if (minSpring is not null && maxSpring is not null)
            {
                minSpring.IsEdge = true;
                maxSpring.IsEdge = true;
            }
        }
    }
}
