﻿using System;
using System.Collections.Generic;

namespace SoftBodyPhysics.Model;

public interface IHardBody : IBody
{
    IReadOnlyCollection<IEdge> Edges { get; }
}

internal class HardBody : IHardBody
{
    #region IHardBody
    IReadOnlyCollection<IEdge> IHardBody.Edges => Edges;
    #endregion

    public int Index;

    public Edge[] Edges;

    public readonly Borders Borders;

    public HardBody()
    {
        Edges = Array.Empty<Edge>();
        Borders = new();
    }
}
