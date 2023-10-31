using System;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Core;

namespace SoftBodyPhysics.Intersections;

internal interface ISegmentIntersectDetector
{
    bool Intersected(Vector lineFrom, Vector lineTo, Vector point);
}

internal class SegmentIntersectDetector : ISegmentIntersectDetector
{
    private const double _delta = 0.0001;
    private readonly ISegmentChecker _segmentChecker;

    public SegmentIntersectDetector(
        ISegmentChecker segmentChecker)
    {
        _segmentChecker = segmentChecker;
    }

    public bool Intersected(Vector lineFrom, Vector lineTo, Vector point)
    {
        // https://e-maxx.ru/algo/circle_line_intersection

        // двигаем точку в начало координат
        var lineFromX = lineFrom.x - point.x;
        var lineFromY = lineFrom.y - point.y;
        var lineToX = lineTo.x - point.x;
        var lineToY = lineTo.y - point.y;

        // уравнение прямой
        var a = lineFromY - lineToY;
        var b = lineToX - lineFromX;
        var c = lineFromX * lineToY - lineToX * lineFromY;
        var r = Constants.MassPointRadius;

        var m = a * a + b * b;
        var x0 = -a * c / m;
        var y0 = -b * c / m;

        if (c * c > r * r * m + _delta) return false;

        if (Math.Abs(c * c - r * r * m) < _delta)
        {
            return _segmentChecker.IsPointInSegment(lineFrom, lineTo, new(x0 + point.x, y0 + point.y));
        }

        var d = r * r - c * c / m;
        var mult = (float)Math.Sqrt(d / m);
        var ax = x0 + b * mult;
        var ay = y0 - a * mult;

        return _segmentChecker.IsPointInSegment(lineFrom, lineTo, new(ax + point.x, ay + point.y));
    }
}
