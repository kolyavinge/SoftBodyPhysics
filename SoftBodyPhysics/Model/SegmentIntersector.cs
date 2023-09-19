using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

internal interface ISegmentIntersector
{
    Vector2d? GetIntersectPoint(Vector2d point1From, Vector2d point1To, Vector2d point2From, Vector2d point2To);
}

internal class SegmentIntersector : ISegmentIntersector
{
    private const double _delta = 0.00001;
    private readonly ILineIntersector _lineIntersector;

    public SegmentIntersector(ILineIntersector lineIntersector)
    {
        _lineIntersector = lineIntersector;
    }

    public Vector2d? GetIntersectPoint(Vector2d line1From, Vector2d line1To, Vector2d line2From, Vector2d line2To)
    {
        var point = _lineIntersector.GetIntersectPoint(line1From, line1To, line2From, line2To);
        if (point == null) return null;
        if (InSegment(line1From, line1To, point.Value) && InSegment(line2From, line2To, point.Value))
        {
            return point;
        }
        else
        {
            return null;
        }
    }

    private bool InSegment(Vector2d lineFrom, Vector2d lineTo, Vector2d point)
    {
        var (minX, maxX) = lineFrom.X < lineTo.X ? (lineFrom.X, lineTo.X) : (lineTo.X, lineFrom.X);
        var (minY, maxY) = lineFrom.Y < lineTo.Y ? (lineFrom.Y, lineTo.Y) : (lineTo.Y, lineFrom.Y);

        minX -= _delta;
        minY -= _delta;
        maxX += _delta;
        maxY += _delta;

        return (minX <= point.X && point.X <= maxX) && (minY <= point.Y && point.Y <= maxY);
    }
}
