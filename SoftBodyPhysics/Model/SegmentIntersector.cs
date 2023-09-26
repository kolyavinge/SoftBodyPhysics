using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

internal interface ISegmentIntersector
{
    Vector? GetIntersectPoint(Vector point1From, Vector point1To, Vector point2From, Vector point2To);
}

internal class SegmentIntersector : ISegmentIntersector
{
    private readonly ILineIntersector _lineIntersector;
    private readonly ISegmentDetector _segmentDetector;

    public SegmentIntersector(ILineIntersector lineIntersector, ISegmentDetector segmentDetector)
    {
        _lineIntersector = lineIntersector;
        _segmentDetector = segmentDetector;
    }

    public Vector? GetIntersectPoint(Vector line1From, Vector line1To, Vector line2From, Vector line2To)
    {
        var point = _lineIntersector.GetIntersectPoint(line1From, line1To, line2From, line2To);
        if (point is null) return null;
        if (_segmentDetector.InSegment(line1From, line1To, point.Value) && _segmentDetector.InSegment(line2From, line2To, point.Value))
        {
            return point;
        }
        else
        {
            return null;
        }
    }
}
