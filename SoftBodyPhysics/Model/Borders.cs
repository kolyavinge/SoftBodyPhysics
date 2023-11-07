namespace SoftBodyPhysics.Model;

internal class Borders
{
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float HalfWidth;
    public float HalfHeight;
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
        HalfWidth = Width / 2.0f;
        HalfHeight = Height / 2.0f;
        MiddleX = minX + HalfWidth;
        MiddleY = minY + HalfHeight;
    }
}
