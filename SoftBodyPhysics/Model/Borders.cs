using System.Runtime.CompilerServices;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

internal class Borders
{
    public static readonly Borders Default = new(0, 0, 0, 0);

    public readonly double MinX;
    public readonly double MaxX;
    public readonly double MinY;
    public readonly double MaxY;
    public readonly double Width;
    public readonly double Height;
    public readonly double WidthHalf;
    public readonly double HeightHalf;
    public readonly double MiddleX;
    public readonly double MiddleY;

    public Borders(double minX, double maxX, double minY, double maxY)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
        Width = maxX - minX;
        Height = maxY - minY;
        WidthHalf = Width / 2.0;
        HeightHalf = Height / 2.0;
        MiddleX = minX + WidthHalf;
        MiddleY = minY + HeightHalf;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPointIn(Borders borders, Vector point, double delta)
    {
        return
            borders.MinX - delta < point.X && point.X < borders.MaxX + delta &&
            borders.MinY - delta < point.Y && point.Y < borders.MaxY + delta;

    }
}
