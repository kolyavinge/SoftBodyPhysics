using System;

namespace SoftBodyPhysics.Geo;

internal interface IVectorCalculator
{
    Vector GetNormalVector(Vector lineFrom, Vector lineTo);
    void ReflectVector(Vector vector, Vector normal);
}

internal class VectorCalculator : IVectorCalculator
{
    private const double _halfPI = Math.PI / 2.0;
    private const double _delta = 0.00001;

    public Vector GetNormalVector(Vector lineFrom, Vector lineTo)
    {
        if (Math.Abs(lineFrom.y - lineTo.y) < _delta)
        {
            return new(0, 1);
        }
        else if (Math.Abs(lineFrom.x - lineTo.x) < _delta)
        {
            return new(1, 0);
        }
        else
        {
            var (sortedFrom, sortedTo) = lineFrom.x < lineTo.x ? (lineFrom, lineTo) : (lineTo, lineFrom);
            var alpha = Math.Abs(Math.Atan((sortedTo.y - sortedFrom.y) / (sortedTo.x - sortedFrom.x)));
            if (sortedTo.y < sortedFrom.y) alpha = -alpha;
            var x = (float)Math.Cos(_halfPI + alpha);
            var y = (float)Math.Sin(_halfPI + alpha);

            return new(x, y);
        }
    }

    public void ReflectVector(Vector vector, Vector normal)
    {
        // vector - 2.0f * (vector * normal) * normal
        var product = 2.0f * (vector.x * normal.x + vector.y * normal.y);
        vector.x -= product * normal.x;
        vector.y -= product * normal.y;
    }
}
