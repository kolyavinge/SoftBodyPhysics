using System;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Core;

namespace SoftBodyPhysics.Intersections;

internal interface ISegmentIntersector
{
    bool GetIntersectPoint(Vector segment1From, Vector segment1To, Vector segment2From, Vector segment2To, Vector outputResult);
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

    public bool GetIntersectPoint(Vector segment1From, Vector segment1To, Vector segment2From, Vector segment2To, Vector outputResult)
    {
        if (!_lineIntersector.GetIntersectPoint(segment1From, segment1To, segment2From, segment2To, outputResult)) return false;
        return IsPointInSegment(segment1From, segment1To, outputResult.x, outputResult.y) && IsPointInSegment(segment2From, segment2To, outputResult.x, outputResult.y);
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
        var c2 = c * c;
        var r2 = _r * _r;

        if (c2 > r2 * m + _delta) return false;

        var d = r2 - c2 / m;
        var mult = (float)Math.Sqrt(d / m);
        var ax = x0 + b * mult;
        var ay = y0 - a * mult;

        return IsPointInSegment(segmentFrom, segmentTo, ax + point.x, ay + point.y);
    }

    private bool IsPointInSegment(Vector segmentFrom, Vector segmentTo, float pointX, float pointY)
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
            minX <= pointX && pointX <= maxX &&
            minY <= pointY && pointY <= maxY;
    }
}
