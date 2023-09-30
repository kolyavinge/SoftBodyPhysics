using System;
using System.Linq;

namespace SoftBodyPhysics.Geo;

internal static class Vectors
{
    public static double GetMaxY(params Vector[] vectors)
    {
        if (!vectors.Any()) throw new ArgumentException();

        var result = vectors[0].Y;

        foreach (var v in vectors.Skip(1))
        {
            result = Math.Max(result, v.Y);
        }

        return result;
    }
}
