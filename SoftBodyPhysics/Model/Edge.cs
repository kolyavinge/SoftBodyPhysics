using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

public interface IEdge
{
    Vector From { get; }

    Vector To { get; }
}

internal class Edge : IEdge
{
    public Vector From { get; }

    public Vector To { get; }

    public Edge(Vector from, Vector to)
    {
        From = from;
        To = to;
    }
}
