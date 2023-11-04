using System.Collections.Generic;
using SoftBodyPhysics.Calculations;

namespace SoftBodyPhysics.Model;

public interface IEdge : ISegment, IBarrier
{
    Vector From { get; }

    Vector To { get; }

    IReadOnlyCollection<IMassPoint> Collisions { get; }

    object? Tag { get; set; }
}

internal class Edge : IEdge
{
    #region IEdge
    public Vector FromPosition => From;
    public Vector ToPosition => To;
    Vector IEdge.From => From;
    Vector IEdge.To => To;
    IReadOnlyCollection<IMassPoint> IEdge.Collisions => Collisions;
    public object? Tag { get; set; }
    #endregion

    // поля для оптимизации

    public readonly Vector From;

    public readonly Vector To;

    public readonly List<IMassPoint> Collisions;

    public Edge(Vector from, Vector to)
    {
        From = from;
        To = to;
        Collisions = new List<IMassPoint>();
    }
}
