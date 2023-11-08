using SoftBodyPhysics.Calculations;

namespace SoftBodyPhysics.Model;

public interface IEdge : ISegment
{
    Vector From { get; }

    Vector To { get; }

    object? Tag { get; set; }
}

internal class Edge : IEdge
{
    #region IEdge
    public Vector FromPosition => From;
    public Vector ToPosition => To;
    Vector IEdge.From => From;
    Vector IEdge.To => To;
    public object? Tag { get; set; }
    #endregion

    // поля для оптимизации

    public readonly Vector From;

    public readonly Vector To;

    public Edge(Vector from, Vector to)
    {
        From = from;
        To = to;
    }
}
