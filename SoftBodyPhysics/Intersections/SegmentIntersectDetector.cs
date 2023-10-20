using System;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Intersections;

internal interface ISegmentIntersectDetector
{
    bool Intersected(Vector lineFrom, Vector lineTo, Vector point);
}

internal class SegmentIntersectDetector : ISegmentIntersectDetector
{
    private const double _delta = 0.0001;
    private readonly ISegmentChecker _segmentChecker;
    private readonly IPhysicsUnits _physicsUnits;

    public SegmentIntersectDetector(
        ISegmentChecker segmentChecker,
        IPhysicsUnits physicsUnits)
    {
        _segmentChecker = segmentChecker;
        _physicsUnits = physicsUnits;
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
        var r = _physicsUnits.MassPointRadius;

        var x0 = -a * c / (a * a + b * b);
        var y0 = -b * c / (a * a + b * b);

        if (c * c > r * r * (a * a + b * b) + _delta) return false;

        if (Math.Abs(c * c - r * r * (a * a + b * b)) < _delta)
        {
            return _segmentChecker.IsPointInSegment(lineFrom, lineTo, new(x0 + point.x, y0 + point.y));
        }

        var d = r * r - c * c / (a * a + b * b);
        var mult = Math.Sqrt(d / (a * a + b * b));
        var ax = (float)(x0 + b * mult);
        var ay = (float)(y0 - a * mult);
        //var bx = x0 - b * mult;
        //var by = y0 + a * mult;

        return _segmentChecker.IsPointInSegment(lineFrom, lineTo, new(ax + point.x, ay + point.y));
    }
}
