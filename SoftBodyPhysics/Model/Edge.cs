using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

public interface IEdge
{
    Vector2d From { get; }

    Vector2d To { get; }
}

internal class Edge : IEdge
{
    public Vector2d From { get; }

    public Vector2d To { get; }

    public Edge(Vector2d from, Vector2d to)
    {
        From = from;
        To = to;
    }
}
