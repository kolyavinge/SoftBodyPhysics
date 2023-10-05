using System.Collections.Generic;
using System.Linq;

namespace SoftBodyPhysics.Model;

internal interface ISoftBodiesCollection
{
    IReadOnlyList<SoftBody> SoftBodies { get; }

    IReadOnlyCollection<MassPoint> AllMassPoints { get; }

    IReadOnlyCollection<Spring> AllSprings { get; }

    void AddSoftBodies(IEnumerable<SoftBody> softBodies);
}

internal class SoftBodiesCollection : ISoftBodiesCollection
{
    private readonly List<SoftBody> _softBodies;

    public IReadOnlyList<SoftBody> SoftBodies => _softBodies;

    public SoftBodiesCollection()
    {
        _softBodies = new List<SoftBody>();
    }

    public IReadOnlyCollection<MassPoint> AllMassPoints { get; private set; }

    public IReadOnlyCollection<Spring> AllSprings { get; private set; }

    public void AddSoftBodies(IEnumerable<SoftBody> softBodies)
    {
        _softBodies.AddRange(softBodies);
        AllMassPoints = _softBodies.SelectMany(x => x.MassPoints).ToList();
        AllSprings = _softBodies.SelectMany(x => x.Springs).ToList();
    }
}
