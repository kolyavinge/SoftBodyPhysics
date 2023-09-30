using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Intersections;

internal interface ISpringIntersector
{
    Vector? GetIntersectPoint(Spring spring, Vector massPointPosition);
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

    public Vector? GetIntersectPoint(Spring spring, Vector massPointPosition)
    {
        var maxY = Vectors.GetMaxY(spring.PointA.Position, spring.PointB.Position, spring.PointA.PrevPosition, spring.PointB.PrevPosition, massPointPosition);

        var polygonPoints = new[]
        {
            (spring.PointA.Position, spring.PointB.Position),
            (spring.PointA.PrevPosition, spring.PointB.PrevPosition),
            (spring.PointA.PrevPosition, spring.PointA.Position),
            (spring.PointB.PrevPosition, spring.PointB.Position),
        };

        if (!_polygonChecker.IsPointInPolygon(polygonPoints, massPointPosition, maxY)) return null;

        var minY = Vectors.GetMinY(spring.PointA.Position, spring.PointB.Position, spring.PointA.PrevPosition, spring.PointB.PrevPosition);
        var minX = Vectors.GetMinX(spring.PointA.Position, spring.PointB.Position, spring.PointA.PrevPosition, spring.PointB.PrevPosition);
        var maxX = Vectors.GetMaxX(spring.PointA.Position, spring.PointB.Position, spring.PointA.PrevPosition, spring.PointB.PrevPosition);

        var intersectPoint =
            _segmentIntersector.GetIntersectPoint(spring.PointA.Position, spring.PointB.Position, massPointPosition, new(massPointPosition.X, maxY)) ??
            _segmentIntersector.GetIntersectPoint(spring.PointA.Position, spring.PointB.Position, massPointPosition, new(massPointPosition.X, minY)) ??
            _segmentIntersector.GetIntersectPoint(spring.PointA.Position, spring.PointB.Position, massPointPosition, new(minX, massPointPosition.Y)) ??
            _segmentIntersector.GetIntersectPoint(spring.PointA.Position, spring.PointB.Position, massPointPosition, new(maxX, massPointPosition.Y));

        return intersectPoint;
    }
}
