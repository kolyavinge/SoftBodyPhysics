using System.Collections.Generic;

namespace SoftBodyPhysics.Model;

internal interface IHardBodiesCollection
{
    IReadOnlyList<HardBody> HardBodies { get; }

    IEnumerable<Edge> AllEdges { get; }

    void AddHardBody(HardBody hardBody);
}

internal class HardBodiesCollection : IHardBodiesCollection
{
    private readonly List<HardBody> _hardBodies;

    public IReadOnlyList<HardBody> HardBodies => _hardBodies;

    public HardBodiesCollection()
    {
        _hardBodies = new List<HardBody>();
    }

    public IEnumerable<Edge> AllEdges
    {
        get
        {
            foreach (var body in _hardBodies)
            {
                foreach (var edge in body.Edges)
                {
                    yield return edge;
                }
            }
        }
    }

    public void AddHardBody(HardBody hardBody)
    {
        _hardBodies.Add(hardBody);
    }
}
