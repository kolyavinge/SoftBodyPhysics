using System.Collections.Generic;
using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Intersections;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

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
                    if (p.Y < minY)
                    {
                        minY = p.Y;
                        minSpring = spring;
                    }
                    else if (p.Y > maxY)
                    {
                        maxY = p.Y;
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
                    if (p.X < minX)
                    {
                        minX = p.X;
                        minSpring = spring;
                    }
                    else if (p.X > maxX)
                    {
                        maxX = p.X;
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
