using System;

namespace SoftBodyPhysics.Geo;

internal interface IVectorCalculator
{
    Vector GetNormalVector(Vector lineFrom, Vector lineTo);
    Vector GetReflectedVector(Vector vector, Vector normal);
}

internal class VectorCalculator : IVectorCalculator
{
    private const double _halfPI = Math.PI / 2.0;
    private const double _delta = 0.00001;

    public Vector GetNormalVector(Vector lineFrom, Vector lineTo)
    {
        if (Math.Abs(lineFrom.Y - lineTo.Y) < _delta)
        {
            return new(0, 1);
        }
        else if (Math.Abs(lineFrom.X - lineTo.X) < _delta)
        {
            return new(1, 0);
        }
        else
        {
            var (sortedFrom, sortedTo) = lineFrom.X < lineTo.X ? (lineFrom, lineTo) : (lineTo, lineFrom);
            var alpha = Math.Abs(Math.Atan((sortedTo.Y - sortedFrom.Y) / (sortedTo.X - sortedFrom.X)));
            if (sortedTo.Y < sortedFrom.Y) alpha = -alpha;
            var x = (float)Math.Cos(_halfPI + alpha);
            var y = (float)Math.Sin(_halfPI + alpha);

            return new(x, y);
        }
    }

    public Vector GetReflectedVector(Vector vector, Vector normal)
    {
        return vector - 2.0f * (vector * normal) * normal;
    }
}
