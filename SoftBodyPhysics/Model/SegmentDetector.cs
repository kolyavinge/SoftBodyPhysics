using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

internal interface ISegmentDetector
{
    bool InSegment(Vector lineFrom, Vector lineTo, Vector point);
}

internal class SegmentDetector : ISegmentDetector
{
    private const double _delta = 0.0001;

    public bool InSegment(Vector lineFrom, Vector lineTo, Vector point)
    {
        var (minX, maxX) = lineFrom.X < lineTo.X ? (lineFrom.X, lineTo.X) : (lineTo.X, lineFrom.X);
        var (minY, maxY) = lineFrom.Y < lineTo.Y ? (lineFrom.Y, lineTo.Y) : (lineTo.Y, lineFrom.Y);

        minX -= _delta;
        maxX += _delta;
        minY -= _delta;
        maxY += _delta;

        return
            (minX <= point.X && point.X <= maxX) &&
            (minY <= point.Y && point.Y <= maxY);
    }
}
