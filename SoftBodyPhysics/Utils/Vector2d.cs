using System;

namespace SoftBodyPhysics.Utils;

public readonly struct Vector2d
{
    public static readonly Vector2d Zero = new(0, 0);

    public readonly double X;
    public readonly double Y;

    public Vector2d(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double Length => Math.Sqrt(X * X + Y * Y);

    public Vector2d Normalized
    {
        get
        {
            var length = Length;
            return new(X / length, Y / length);
        }
    }

    public static Vector2d operator +(Vector2d a, Vector2d b) => new(a.X + b.X, a.Y + b.Y);

    public static Vector2d operator -(Vector2d a, Vector2d b) => new(a.X - b.X, a.Y - b.Y);

    public static Vector2d operator *(Vector2d a, double k) => new(k * a.X, k * a.Y);

    public static Vector2d operator *(double k, Vector2d a) => a * k;

    public static double operator *(Vector2d a, Vector2d b) => a.X * b.X + a.Y * b.Y;

    public override string ToString() => $"{X:F2} : {Y:F2}";
}
