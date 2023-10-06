using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftBodyPhysics.Model;

internal interface IHardBodiesCollection
{
    IReadOnlyList<HardBody> HardBodies { get; }

    Edge[] AllEdges { get; }

    void AddHardBodies(IEnumerable<HardBody> hardBodies);
}

internal class HardBodiesCollection : IHardBodiesCollection
{
    private readonly List<HardBody> _hardBodies;

    public IReadOnlyList<HardBody> HardBodies => _hardBodies;

    public Edge[] AllEdges { get; private set; }

    public HardBodiesCollection()
    {
        _hardBodies = new List<HardBody>();
        AllEdges = Array.Empty<Edge>();
    }

    public void AddHardBodies(IEnumerable<HardBody> hardBodies)
    {
        _hardBodies.AddRange(hardBodies);
        AllEdges = _hardBodies.SelectMany(x => x.Edges).ToArray();
    }
}
