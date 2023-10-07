using System;
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

    public Edge[] Edges;

    public Borders Borders;

    public HardBody()
    {
        Edges = Array.Empty<Edge>();
        Borders = Borders.Default;
    }
}
