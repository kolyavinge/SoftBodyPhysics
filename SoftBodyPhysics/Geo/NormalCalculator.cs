using System;

namespace SoftBodyPhysics.Geo;

internal interface INormalCalculator
{
    Vector GetNormal(Vector lineFrom, Vector lineTo);
}

internal class NormalCalculator : INormalCalculator
{
    private const double _halfPI = Math.PI / 2.0;

    public Vector GetNormal(Vector lineFrom, Vector lineTo)
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
