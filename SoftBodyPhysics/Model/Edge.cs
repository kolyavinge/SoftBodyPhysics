using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

public interface IEdge : ISegment
{
    Vector From { get; }

    Vector To { get; }

    CollisionState State { get; }
}

internal class Edge : IEdge
{
    #region IEdge
    public Vector FromPosition => From;
    public Vector ToPosition => To;
    #endregion

    #region IEdge
    Vector IEdge.From => From;
    Vector IEdge.To => To;
    CollisionState IEdge.State => State;
    #endregion

    // поля для оптимизации

    public Vector From;

    public Vector To;

    public CollisionState State;

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
