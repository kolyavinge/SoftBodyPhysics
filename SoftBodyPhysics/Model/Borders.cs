using System.Runtime.CompilerServices;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

internal class Borders
{
    public static readonly Borders Default = new(0, 0, 0, 0);

    public readonly float MinX;
    public readonly float MaxX;
    public readonly float MinY;
    public readonly float MaxY;
    public readonly float Width;
    public readonly float Height;
    public readonly float WidthHalf;
    public readonly float HeightHalf;
    public readonly float MiddleX;
    public readonly float MiddleY;

    public Borders(float minX, float maxX, float minY, float maxY)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
        Width = maxX - minX;
        Height = maxY - minY;
        WidthHalf = Width / 2.0f;
        HeightHalf = Height / 2.0f;
        MiddleX = minX + WidthHalf;
        MiddleY = minY + HeightHalf;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPointIn(Borders borders, Vector point, float delta)
    {
        return
            borders.MinX - delta < point.X && point.X < borders.MaxX + delta &&
            borders.MinY - delta < point.Y && point.Y < borders.MaxY + delta;
    }
}
