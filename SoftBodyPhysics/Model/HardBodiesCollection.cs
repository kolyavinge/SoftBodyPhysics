using System.Collections.Generic;
using System.Linq;

namespace SoftBodyPhysics.Model;

internal interface IHardBodiesCollection
{
    IReadOnlyList<HardBody> HardBodies { get; }

    IReadOnlyCollection<Edge> AllEdges { get; }

    void AddHardBodies(IEnumerable<HardBody> hardBodies);
}

internal class HardBodiesCollection : IHardBodiesCollection
{
    private readonly List<HardBody> _hardBodies;

    public IReadOnlyList<HardBody> HardBodies => _hardBodies;

    public HardBodiesCollection()
    {
        _hardBodies = new List<HardBody>();
    }

    public IReadOnlyCollection<Edge> AllEdges { get; private set; }

    public void AddHardBodies(IEnumerable<HardBody> hardBodies)
    {
        _hardBodies.AddRange(hardBodies);
        AllEdges = _hardBodies.SelectMany(x => x.Edges).ToList();
    }
}
