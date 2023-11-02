﻿using System;
using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Calculations;

namespace SoftBodyPhysics.Model;

public interface ISoftBody
{
    IReadOnlyCollection<IMassPoint> MassPoints { get; }

    IReadOnlyCollection<ISpring> Springs { get; }

    Vector MiddlePoint { get; }
}

internal class SoftBody : ISoftBody
{
    #region ISoftBody
    IReadOnlyCollection<IMassPoint> ISoftBody.MassPoints => MassPoints;
    IReadOnlyCollection<ISpring> ISoftBody.Springs => Springs;
    Vector ISoftBody.MiddlePoint => new(Borders.MiddleX, Borders.MiddleY);
    #endregion

    // массивы а не списки для оптимизации

    public MassPoint[] MassPoints;

    public Spring[] Springs;

    public Spring[] Edges;

    public readonly Borders Borders;

    public bool IsMoving;

    public SoftBody()
    {
        MassPoints = Array.Empty<MassPoint>();
        Springs = Array.Empty<Spring>();
        Edges = Array.Empty<Spring>();
        Borders = new();
        IsMoving = true;
    }

    public void UpdateEdges()
    {
        Edges = Springs.Where(x => x.IsEdge).ToArray();
    }
}
