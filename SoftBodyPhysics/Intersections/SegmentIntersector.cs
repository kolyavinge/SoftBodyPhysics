using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Intersections;

internal interface ISegmentIntersector
{
    Vector? GetIntersectPoint(Vector point1From, Vector point1To, Vector point2From, Vector point2To);
}

internal class SegmentIntersector : ISegmentIntersector
{
    private readonly ILineIntersector _lineIntersector;
    private readonly ISegmentChecker _segmentChecker;

    public SegmentIntersector(ILineIntersector lineIntersector, ISegmentChecker segmentChecker)
    {
        _lineIntersector = lineIntersector;
        _segmentChecker = segmentChecker;
    }

    public Vector? GetIntersectPoint(Vector line1From, Vector line1To, Vector line2From, Vector line2To)
    {
        var point = _lineIntersector.GetIntersectPoint(line1From, line1To, line2From, line2To);
        if (point is null) return null;
        if (_segmentChecker.IsPointInSegment(line1From, line1To, point) && _segmentChecker.IsPointInSegment(line2From, line2To, point))
        {
            return point;
        }
        else
        {
            return null;
        }
    }
}
