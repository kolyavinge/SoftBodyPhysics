using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

public interface IEdge
{
    Vector From { get; }

    Vector To { get; }

    CollisionState State { get; }
}

internal class Edge : IEdge
{
    public Vector From { get; }

    public Vector To { get; }

    public CollisionState State { get; set; }

    public Edge(Vector from, Vector to)
    {
        From = from;
        To = to;
    }

    public void ResetState()
    {
        State = CollisionState.Normal;
    }
}
