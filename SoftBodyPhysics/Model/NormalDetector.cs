using System;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

internal interface INormalDetector
{
    Vector2d GetNormal(Vector2d lineFrom, Vector2d lineTo);
}

internal class NormalDetector : INormalDetector
{
    private const double _halfPI = Math.PI / 2.0;

    public Vector2d GetNormal(Vector2d lineFrom, Vector2d lineTo)
    {
        if (lineFrom.Y == lineTo.Y)
        {
            return new(0, 1);
        }
        else if (lineFrom.X == lineTo.X)
        {
            return new(1, 0);
        }
        else
        {
            var (sortedFrom, sortedTo) = lineFrom.X < lineTo.X ? (lineFrom, lineTo) : (lineTo, lineFrom);
            var alpha = Math.Abs(Math.Atan((sortedTo.Y - sortedFrom.Y) / (sortedTo.X - sortedFrom.X)));
            if (sortedTo.Y < sortedFrom.Y) alpha = -alpha;
            var x = Math.Cos(_halfPI + alpha);
            var y = Math.Sin(_halfPI + alpha);

            return new(x, y);
        }
    }
}
