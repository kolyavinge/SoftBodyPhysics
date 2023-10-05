﻿using System;
using System.Linq;

namespace SoftBodyPhysics.Geo;

internal static class Vectors
{
    public static double GetMinX(params Vector[] vectors)
    {
        if (!vectors.Any()) throw new ArgumentException();

        var result = vectors[0].X;
        for (int i = 1; i < vectors.Length; i++)
        {
            result = Math.Min(result, vectors[i].X);
        }

        return result;
    }

    public static double GetMaxX(params Vector[] vectors)
    {
        if (!vectors.Any()) throw new ArgumentException();

        var result = vectors[0].X;
        for (int i = 1; i < vectors.Length; i++)
        {
            result = Math.Max(result, vectors[i].X);
        }

        return result;
    }

    public static double GetMinY(params Vector[] vectors)
    {
        if (!vectors.Any()) throw new ArgumentException();

        var result = vectors[0].Y;
        for (int i = 1; i < vectors.Length; i++)
        {
            result = Math.Min(result, vectors[i].Y);
        }

        return result;
    }

    public static double GetMaxY(params Vector[] vectors)
    {
        if (!vectors.Any()) throw new ArgumentException();

        var result = vectors[0].Y;
        for (int i = 1; i < vectors.Length; i++)
        {
            result = Math.Max(result, vectors[i].Y);
        }

        return result;
    }
}