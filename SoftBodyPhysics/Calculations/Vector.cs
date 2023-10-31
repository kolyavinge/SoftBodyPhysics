using System;
using System.Collections.Generic;

namespace SoftBodyPhysics.Calculations;

public class Vector : IEquatable<Vector?>
{
    public float X => x;
    public float Y => y;

    // внутри сборки использовать эти поля
    internal float x;
    internal float y;

    public Vector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public float Length => (float)Math.Sqrt(x * x + y * y);

    public Vector Unit
    {
        get
        {
            var length = Length;
            if (length == 0) return new(0, 0);

            return new(x / length, y / length);
        }
    }

    // для производительности операции лучше не использовать

    public static Vector operator +(Vector a, Vector b) => new(a.x + b.x, a.y + b.y);

    public static Vector operator -(Vector a, Vector b) => new(a.x - b.x, a.y - b.y);

    public static Vector operator *(Vector a, float k) => new(k * a.x, k * a.y);

    public static Vector operator *(float k, Vector a) => a * k;

    public static bool operator ==(Vector? left, Vector? right) => EqualityComparer<Vector>.Default.Equals(left!, right!);

    public static bool operator !=(Vector? left, Vector? right) => !(left == right);

    public Vector Clone()
    {
        return new(x, y);
    }

    public override string ToString()
    {
        return $"{x:F2} : {y:F2}";
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Vector);
    }

    public bool Equals(Vector? other)
    {
        return other is not null &&
               x == other.x &&
               y == other.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    internal static float GetDistanceBetween(Vector a, Vector b)
    {
        var dx = a.x - b.x;
        var dy = a.y - b.y;
        var distance = (float)Math.Sqrt(dx * dx + dy * dy);

        return distance;
    }
}
