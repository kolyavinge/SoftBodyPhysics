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

    public readonly List<MassPoint> MassPoints;

    public readonly List<Spring> Springs;

    public List<Spring> Edges;

    public Borders Borders;

    public SoftBody()
    {
        MassPoints = new List<MassPoint>();
        Springs = new List<Spring>();
    }

    public void UpdateEdges()
    {
        Edges = Springs.Where(x => x.IsEdge).ToList();
    }
}
