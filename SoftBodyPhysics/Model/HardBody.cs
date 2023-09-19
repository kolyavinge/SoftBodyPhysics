using System.Collections.Generic;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

public interface IHardBody
{
    IReadOnlyCollection<IEdge> Edges { get; }

    void AddEdge(Vector2d from, Vector2d to);
}

internal class HardBody : IHardBody
{
    #region IHardBody
    IReadOnlyCollection<IEdge> IHardBody.Edges => Edges;
    #endregion

    public List<Edge> Edges { get; }

    public HardBody()
    {
        Edges = new List<Edge>();
    }

    public void AddEdge(Vector2d from, Vector2d to)
    {
        Edges.Add(new(from, to));
    }
}
