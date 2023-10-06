using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Intersections;

internal interface ISegmentChecker
{
    bool IsPointInSegment(Vector segmentFrom, Vector segementTo, Vector point);
}

internal class SegmentChecker : ISegmentChecker
{
    private const double _delta = 0.00001;

    public bool IsPointInSegment(Vector segmentFrom, Vector segementTo, Vector point)
    {
        var (minX, maxX) = segmentFrom.X < segementTo.X ? (segmentFrom.X, segementTo.X) : (segementTo.X, segmentFrom.X);
        var (minY, maxY) = segmentFrom.Y < segementTo.Y ? (segmentFrom.Y, segementTo.Y) : (segementTo.Y, segmentFrom.Y);

        minX -= _delta;
        maxX += _delta;
        minY -= _delta;
        maxY += _delta;

        return
            minX <= point.X && point.X <= maxX &&
            minY <= point.Y && point.Y <= maxY;
    }
}
