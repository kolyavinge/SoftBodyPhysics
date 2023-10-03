using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

internal readonly struct Borders
{
    public readonly double MinX;
    public readonly double MaxX;
    public readonly double MinY;
    public readonly double MaxY;

    public Borders(double minX, double maxX, double minY, double maxY)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
    }

    public bool IsPointIn(Vector point)
    {
        return
            MinX <= point.X && point.X <= MaxX &&
            MinY <= point.Y && point.Y <= MaxY;
    }
}
