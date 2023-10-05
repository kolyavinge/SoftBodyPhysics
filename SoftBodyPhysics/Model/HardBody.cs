using System.Collections.Generic;

namespace SoftBodyPhysics.Model;

public interface IHardBody
{
    IReadOnlyCollection<IEdge> Edges { get; }
}

internal class HardBody : IHardBody
{
    #region IHardBody
    IReadOnlyCollection<IEdge> IHardBody.Edges => Edges;
    #endregion

    public readonly List<Edge> Edges;

    public HardBody()
    {
        Edges = new List<Edge>();
    }
}
