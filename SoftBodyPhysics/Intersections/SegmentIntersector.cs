using System;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Core;

namespace SoftBodyPhysics.Intersections;

internal interface ISegmentIntersector
{
    Vector? GetIntersectPoint(Vector segment1From, Vector segment1To, Vector segment2From, Vector segment2To);
    bool IsIntersected(Vector segmentFrom, Vector segmentTo, Vector point);
}

internal class SegmentIntersector : ISegmentIntersector
{
    private const float _delta = 0.00001f;
    private const float _r = Constants.MassPointRadius;
    private readonly ILineIntersector _lineIntersector;

    public SegmentIntersector(ILineIntersector lineIntersector)
    {
        _lineIntersector = lineIntersector;
    }

    public Vector? GetIntersectPoint(Vector segment1From, Vector segment1To, Vector segment2From, Vector segment2To)
    {
        var point = _lineIntersector.GetIntersectPoint(segment1From, segment1To, segment2From, segment2To);
        if (point is null) return null;

        return IsPointInSegment(segment1From, segment1To, point) && IsPointInSegment(segment2From, segment2To, point) ? point : null;
    }

    public bool IsIntersected(Vector segmentFrom, Vector segmentTo, Vector point)
    {
        // https://e-maxx.ru/algo/circle_line_intersection

        // двигаем точку в начало координат
        var lineFromX = segmentFrom.x - point.x;
        var lineFromY = segmentFrom.y - point.y;
        var lineToX = segmentTo.x - point.x;
        var lineToY = segmentTo.y - point.y;

        // уравнение прямой
        var a = lineFromY - lineToY;
        var b = lineToX - lineFromX;
        var c = lineFromX * lineToY - lineToX * lineFromY;

        var m = a * a + b * b;
        var x0 = -a * c / m;
        var y0 = -b * c / m;

        if (c * c > _r * _r * m + _delta) return false;

        var d = _r * _r - c * c / m;
        var mult = (float)Math.Sqrt(d / m);
        var ax = x0 + b * mult;
        var ay = y0 - a * mult;

        return IsPointInSegment(segmentFrom, segmentTo, new(ax + point.x, ay + point.y));
    }

    private bool IsPointInSegment(Vector segmentFrom, Vector segmentTo, Vector point)
    {
        float minX, maxX, minY, maxY;

        if (segmentFrom.x < segmentTo.x)
        {
            minX = segmentFrom.x;
            maxX = segmentTo.x;
        }
        else
        {
            minX = segmentTo.x;
            maxX = segmentFrom.x;
        }

        if (segmentFrom.y < segmentTo.y)
        {
            minY = segmentFrom.y;
            maxY = segmentTo.y;
        }
        else
        {
            minY = segmentTo.y;
            maxY = segmentFrom.y;
        }

        minX -= _delta;
        maxX += _delta;
        minY -= _delta;
        maxY += _delta;

        return
            minX <= point.x && point.x <= maxX &&
            minY <= point.y && point.y <= maxY;
    }
}
