using System;
using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Calculations;

namespace SoftBodyPhysics.Model;

public interface ISoftBody : IBody
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

    public int Index;

    public MassPoint[] MassPoints;

    public MassPoint[] EdgeMassPoints;

    public Spring[] Springs;

    public Spring[] Edges;

    public readonly Borders Borders;

    public bool IsActive;

    public SoftBody()
    {
        MassPoints = Array.Empty<MassPoint>();
        EdgeMassPoints = Array.Empty<MassPoint>();
        Springs = Array.Empty<Spring>();
        Edges = Array.Empty<Spring>();
        Borders = new();
        IsActive = true;
    }

    public void UpdateEdges()
    {
        Edges = Springs.Where(x => x.IsEdge).ToArray();
        EdgeMassPoints = Edges.Select(x => x.PointA).Union(Edges.Select(x => x.PointB)).ToArray();
    }
}
