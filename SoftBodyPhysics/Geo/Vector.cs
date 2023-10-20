using System;
using System.Collections.Generic;

namespace SoftBodyPhysics.Geo;

public class Vector : IEquatable<Vector?>
{
    public static readonly Vector Zero = new(0, 0);

    public float X;
    public float Y;

    public Vector(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float Length => (float)Math.Sqrt(X * X + Y * Y);

    // длина в квадрате, экономия на извлечении корня
    public float LengthSquare => X * X + Y * Y;

    public Vector Unit
    {
        get
        {
            var length = Length;
            if (length == 0) return Vector.Zero;

            return new(X / length, Y / length);
        }
    }

    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);

    public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);

    public static Vector operator *(Vector a, float k) => new(k * a.X, k * a.Y);

    public static Vector operator *(float k, Vector a) => a * k;

    public static float operator *(Vector a, Vector b) => a.X * b.X + a.Y * b.Y;

    public static bool operator ==(Vector? left, Vector? right) => EqualityComparer<Vector>.Default.Equals(left!, right!);

    public static bool operator !=(Vector? left, Vector? right) => !(left == right);

    public override string ToString()
    {
        return $"{X:F2} : {Y:F2}";
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Vector);
    }

    public bool Equals(Vector? other)
    {
        return other is not null &&
               X == other.X &&
               Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
