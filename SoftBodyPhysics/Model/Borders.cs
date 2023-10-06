using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

internal class Borders
{
    public static readonly Borders Default = new(0, 0, 0, 0);

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

    public bool IsPointIn(Vector point, double delta)
    {
        return
            MinX - delta <= point.X && point.X <= MaxX + delta &&
            MinY - delta <= point.Y && point.Y <= MaxY + delta;
    }
}
