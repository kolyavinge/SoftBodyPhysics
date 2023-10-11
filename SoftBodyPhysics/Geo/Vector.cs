using System;

namespace SoftBodyPhysics.Geo;

public class Vector
{
    public static readonly Vector Zero = new(0, 0);

    public readonly float X;
    public readonly float Y;

    public Vector(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float Length => (float)Math.Sqrt(X * X + Y * Y);

    // длина в квадрате, экономия на извлечении корня
    public float LengthSquare => X * X + Y * Y;

    public Vector Normalized
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

    public override string ToString() => $"{X:F2} : {Y:F2}";
}
