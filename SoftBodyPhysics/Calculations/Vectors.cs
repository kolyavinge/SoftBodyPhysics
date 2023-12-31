﻿using System;
using System.Linq;

namespace SoftBodyPhysics.Calculations;

internal static class Vectors
{
    public static double GetMinX(params Vector[] vectors)
    {
        if (!vectors.Any()) throw new ArgumentException();

        var result = vectors[0].x;
        for (int i = 1; i < vectors.Length; i++)
        {
            result = MathF.Min(result, vectors[i].x);
        }

        return result;
    }

    public static double GetMaxX(params Vector[] vectors)
    {
        if (!vectors.Any()) throw new ArgumentException();

        var result = vectors[0].x;
        for (int i = 1; i < vectors.Length; i++)
        {
            result = MathF.Max(result, vectors[i].x);
        }

        return result;
    }

    public static double GetMinY(params Vector[] vectors)
    {
        if (!vectors.Any()) throw new ArgumentException();

        var result = vectors[0].y;
        for (int i = 1; i < vectors.Length; i++)
        {
            result = MathF.Min(result, vectors[i].y);
        }

        return result;
    }

    public static double GetMaxY(params Vector[] vectors)
    {
        if (!vectors.Any()) throw new ArgumentException();

        var result = vectors[0].y;
        for (int i = 1; i < vectors.Length; i++)
        {
            result = MathF.Max(result, vectors[i].y);
        }

        return result;
    }
}
