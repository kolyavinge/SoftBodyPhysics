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
    private readonly IBordersCalculator _bordersCalculator;

    public SoftBodySpringEdgeDetector(
        ISegmentIntersector segmentIntersector,
        IBordersCalculator bordersCalculator)
    {
        _segmentIntersector = segmentIntersector;
        _bordersCalculator = bordersCalculator;
    }

    public void DetectEdges(IReadOnlyCollection<SoftBody> softBodies)
    {
        softBodies.Each(DetectEdges);
    }

    public void DetectEdges(SoftBody softBody)
    {
        var borders = _bordersCalculator.GetBordersBySegments(softBody.Springs);
        if (borders is null) return;
        softBody.Springs.Each(s => s.IsEdge = false);
        DetectByVertical(softBody.Springs, borders);
        DetectByHorizontal(softBody.Springs, borders);
        softBody.UpdateEdges();
    }

    private void DetectByVertical(IEnumerable<Spring> springs, Borders borders)
    {
        for (var x = borders.MinX; x <= borders.MaxX; x += _step)
        {
            var lineFrom = new Vector(x, borders.MinY);
            var lineTo = new Vector(x, borders.MaxY);
            var minY = borders.MaxY;
            var maxY = borders.MinY;
            Spring? minSpring = null, maxSpring = null;
            foreach (var spring in springs)
            {
                var p = _segmentIntersector.GetIntersectPoint(spring.PointA.Position, spring.PointB.Position, lineFrom, lineTo);
                if (p is not null)
                {
                    if (p.y < minY)
                    {
                        minY = p.y;
                        minSpring = spring;
                    }
                    if (p.y > maxY)
                    {
                        maxY = p.y;
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
        for (var y = borders.MinY; y <= borders.MaxY; y += _step)
        {
            var lineFrom = new Vector(borders.MinX, y);
            var lineTo = new Vector(borders.MaxX, y);
            var minX = borders.MaxX;
            var maxX = borders.MinX;
            Spring? minSpring = null, maxSpring = null;
            foreach (var spring in springs)
            {
                var p = _segmentIntersector.GetIntersectPoint(spring.PointA.Position, spring.PointB.Position, lineFrom, lineTo);
                if (p is not null)
                {
                    if (p.x < minX)
                    {
                        minX = p.x;
                        minSpring = spring;
                    }
                    if (p.x > maxX)
                    {
                        maxX = p.x;
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
