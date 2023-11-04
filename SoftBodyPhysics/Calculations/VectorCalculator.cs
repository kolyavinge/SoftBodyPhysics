using System;

namespace SoftBodyPhysics.Calculations;

internal interface IVectorCalculator
{
    void GetNormalVector(Vector lineFrom, Vector lineTo, Vector outputResult);
    void ReflectVector(Vector vector, Vector normal);
}

internal class VectorCalculator : IVectorCalculator
{
    private const double _halfPI = Math.PI / 2.0;
    private const double _delta = 0.00001;

    public void GetNormalVector(Vector lineFrom, Vector lineTo, Vector outputResult)
    {
        if (Math.Abs(lineFrom.y - lineTo.y) < _delta)
        {
            outputResult.x = 0;
            outputResult.y = 1;
        }
        else if (Math.Abs(lineFrom.x - lineTo.x) < _delta)
        {
            outputResult.x = 1;
            outputResult.y = 0;
        }
        else
        {
            var (sortedFrom, sortedTo) = lineFrom.x < lineTo.x ? (lineFrom, lineTo) : (lineTo, lineFrom);
            var alpha = Math.Abs(Math.Atan((sortedTo.y - sortedFrom.y) / (sortedTo.x - sortedFrom.x)));
            if (sortedTo.y < sortedFrom.y) alpha = -alpha;
            outputResult.x = (float)Math.Cos(_halfPI + alpha);
            outputResult.y = (float)Math.Sin(_halfPI + alpha);
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
