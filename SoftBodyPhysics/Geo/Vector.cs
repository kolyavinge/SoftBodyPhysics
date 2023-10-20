using System;
using System.Collections.Generic;

namespace SoftBodyPhysics.Geo;

public class Vector : IEquatable<Vector?>
{
    public static readonly Vector Zero = new(0, 0);

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

    internal float LengthSquare => x * x + y * y; // длина в квадрате, экономия на извлечении корня

    public Vector Unit
    {
        get
        {
            var length = Length;
            if (length == 0) return Vector.Zero;

            return new(x / length, y / length);
        }
    }

    public static Vector operator +(Vector a, Vector b) => new(a.x + b.x, a.y + b.y);

    public static Vector operator -(Vector a, Vector b) => new(a.x - b.x, a.y - b.y);

    public static Vector operator *(Vector a, float k) => new(k * a.x, k * a.y);

    public static Vector operator *(float k, Vector a) => a * k;

    public static float operator *(Vector a, Vector b) => a.x * b.x + a.y * b.y;

    public static bool operator ==(Vector? left, Vector? right) => EqualityComparer<Vector>.Default.Equals(left!, right!);

    public static bool operator !=(Vector? left, Vector? right) => !(left == right);

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
}
