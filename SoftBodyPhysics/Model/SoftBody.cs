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

    public Spring[] SpringsToCheckCollisions;

    public Spring[] Edges;

    public Borders Borders;

    public bool IsMoving;

    public SoftBody()
    {
        MassPoints = Array.Empty<MassPoint>();
        Springs = Array.Empty<Spring>();
        SpringsToCheckCollisions = Array.Empty<Spring>();
        Borders = Borders.Default;
        IsMoving = true;
    }

    public void UpdateEdges()
    {
        SpringsToCheckCollisions = Springs.OrderBy(x => x.IsEdge ? 0 : 1).ToArray(); // сначала Edge, потом остальные
        Edges = Springs.Where(x => x.IsEdge).ToArray();
    }
}
