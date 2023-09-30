using System;

namespace SoftBodyPhysics.Geo;

public readonly struct Vector
{
    public static readonly Vector Zero = new(0, 0);

    public readonly double X;
    public readonly double Y;

    public Vector(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double Length => Math.Sqrt(X * X + Y * Y);

    public Vector Normalized
    {
        get
        {
            var length = Length;
            return new(X / length, Y / length);
        }
    }

    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);

    public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);

    public static Vector operator *(Vector a, double k) => new(k * a.X, k * a.Y);

    public static Vector operator *(double k, Vector a) => a * k;

    public static double operator *(Vector a, Vector b) => a.X * b.X + a.Y * b.Y;

    public override string ToString() => $"{X:F2} : {Y:F2}";
}
