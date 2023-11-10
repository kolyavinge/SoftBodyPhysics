using System;

namespace SoftBodyPhysics.Calculations;

internal interface IVectorCalculator
{
    void GetNormalVector(Vector lineFrom, Vector lineTo, Vector outputResult);
    void ReflectVector(Vector vector, Vector normal);
}

internal class VectorCalculator : IVectorCalculator
{
    private const float _halfPI = MathF.PI / 2.0f;
    private const float _delta = 0.00001f;

    public void GetNormalVector(Vector lineFrom, Vector lineTo, Vector outputResult)
    {
        if (MathF.Abs(lineFrom.y - lineTo.y) < _delta)
        {
            outputResult.x = 0;
            outputResult.y = 1;
        }
        else if (MathF.Abs(lineFrom.x - lineTo.x) < _delta)
        {
            outputResult.x = 1;
            outputResult.y = 0;
        }
        else
        {
            var (sortedFrom, sortedTo) = lineFrom.x < lineTo.x ? (lineFrom, lineTo) : (lineTo, lineFrom);
            var alpha = MathF.Abs(MathF.Atan((sortedTo.y - sortedFrom.y) / (sortedTo.x - sortedFrom.x)));
            if (sortedTo.y < sortedFrom.y) alpha = -alpha;
            outputResult.x = MathF.Cos(_halfPI + alpha);
            outputResult.y = MathF.Sin(_halfPI + alpha);
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
