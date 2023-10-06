using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftBodyPhysics.Model;

public interface ISoftBody
{
    IReadOnlyCollection<IMassPoint> MassPoints { get; }

    IReadOnlyCollection<ISpring> Springs { get; }
}

internal class SoftBody : ISoftBody
{
    #region ISoftBody
    IReadOnlyCollection<IMassPoint> ISoftBody.MassPoints => MassPoints;
    IReadOnlyCollection<ISpring> ISoftBody.Springs => Springs;
    #endregion

    // массивы а не списки для оптимизации

    public MassPoint[] MassPoints;

    public Spring[] Springs;

    public Spring[] Edges;

    public Borders Borders;

    public SoftBody()
    {
        MassPoints = Array.Empty<MassPoint>();
        Springs = Array.Empty<Spring>();
        Edges = Array.Empty<Spring>();
        Borders = Borders.Default;
    }

    public void UpdateEdges()
    {
        Edges = Springs.Where(x => x.IsEdge).ToArray();
    }
}
