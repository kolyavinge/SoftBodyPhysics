using System;
using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IHardBodiesCollection
{
    HardBody[] HardBodies { get; }

    Edge[] AllEdges { get; }

    void AddHardBodies(IEnumerable<HardBody> hardBodies);
}

internal class HardBodiesCollection : IHardBodiesCollection
{
    private readonly IBodyCollisionCollection _bodyCollisionCollection;
    private readonly List<HardBody> _hardBodies;

    public HardBody[] HardBodies { get; private set; }

    public Edge[] AllEdges { get; private set; }

    public HardBodiesCollection(
        IBodyCollisionCollection bodyCollisionCollection)
    {
        _bodyCollisionCollection = bodyCollisionCollection;
        _hardBodies = new List<HardBody>();
        HardBodies = Array.Empty<HardBody>();
        AllEdges = Array.Empty<Edge>();
    }

    public void AddHardBodies(IEnumerable<HardBody> hardBodies)
    {
        _hardBodies.AddRange(hardBodies);
        var oldHardBodies = HardBodies;
        HardBodies = _hardBodies.ToArray();
        for (int i = 0; i < HardBodies.Length; i++) HardBodies[i].Index = i;
        AllEdges = _hardBodies.SelectMany(x => x.Edges).ToArray();
        _bodyCollisionCollection.UpdateForHardBodies(oldHardBodies, HardBodies);
    }
}
