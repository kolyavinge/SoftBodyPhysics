namespace SoftBodyPhysics.Model;

internal class Borders
{
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float Width;
    public float Height;
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
        MiddleX = minX + Width / 2.0f;
        MiddleY = minY + Height / 2.0f;
    }
}
