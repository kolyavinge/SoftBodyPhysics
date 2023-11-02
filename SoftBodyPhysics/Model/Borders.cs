using System.Runtime.CompilerServices;
using SoftBodyPhysics.Calculations;

namespace SoftBodyPhysics.Model;

internal class Borders
{
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float Width;
    public float Height;
    public float WidthHalf;
    public float HeightHalf;
    public float MiddleX;
    public float MiddleY;

    public void Set(float minX, float maxX, float minY, float maxY)
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
            borders.MinX - delta < point.x && point.x < borders.MaxX + delta &&
            borders.MinY - delta < point.y && point.y < borders.MaxY + delta;
    }
}
