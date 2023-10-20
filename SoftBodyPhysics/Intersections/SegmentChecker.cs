using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Intersections;

internal interface ISegmentChecker
{
    bool IsPointInSegment(Vector segmentFrom, Vector segementTo, Vector point);
}

internal class SegmentChecker : ISegmentChecker
{
    private const float _delta = 0.00001f;

    public bool IsPointInSegment(Vector segmentFrom, Vector segementTo, Vector point)
    {
        var (minX, maxX) = segmentFrom.x < segementTo.x ? (segmentFrom.x, segementTo.x) : (segementTo.x, segmentFrom.x);
        var (minY, maxY) = segmentFrom.y < segementTo.y ? (segmentFrom.y, segementTo.y) : (segementTo.y, segmentFrom.y);

        minX -= _delta;
        maxX += _delta;
        minY -= _delta;
        maxY += _delta;

        return
            minX <= point.x && point.x <= maxX &&
            minY <= point.y && point.y <= maxY;
    }
}
