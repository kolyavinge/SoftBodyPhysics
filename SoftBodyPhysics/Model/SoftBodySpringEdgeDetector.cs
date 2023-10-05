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
    private const double _step = 0.1;
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
        softBody.Springs.Each(s => s.IsEdge = false);
        var borders = _bordersCalculator.GetBorders(softBody.Springs);
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
                    if (p.Value.Y < minY)
                    {
                        minY = p.Value.Y;
                        minSpring = spring;
                    }
                    else if (p.Value.Y > maxY)
                    {
                        maxY = p.Value.Y;
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
                    if (p.Value.X < minX)
                    {
                        minX = p.Value.X;
                        minSpring = spring;
                    }
                    else if (p.Value.X > maxX)
                    {
                        maxX = p.Value.X;
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
