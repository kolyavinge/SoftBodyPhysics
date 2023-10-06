using SoftBodyPhysics.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftBodyPhysics.Model;

internal interface ISoftBodiesCollection
{
    IReadOnlyList<SoftBody> SoftBodies { get; }

    MassPoint[] AllMassPoints { get; }

    Spring[] AllSprings { get; }

    (SoftBody, SoftBody)[] SoftBodiesCrossProduct { get; }

    void AddSoftBodies(IEnumerable<SoftBody> softBodies);
}

internal class SoftBodiesCollection : ISoftBodiesCollection
{
    private readonly List<SoftBody> _softBodies;

    public IReadOnlyList<SoftBody> SoftBodies => _softBodies;

    public MassPoint[] AllMassPoints { get; private set; }

    public Spring[] AllSprings { get; private set; }

    public (SoftBody, SoftBody)[] SoftBodiesCrossProduct { get; private set; }

    public SoftBodiesCollection()
    {
        _softBodies = new List<SoftBody>();
        AllMassPoints = Array.Empty<MassPoint>();
        AllSprings = Array.Empty<Spring>();
        SoftBodiesCrossProduct = Array.Empty<(SoftBody, SoftBody)>();
    }

    public void AddSoftBodies(IEnumerable<SoftBody> softBodies)
    {
        _softBodies.AddRange(softBodies);
        AllMassPoints = _softBodies.SelectMany(x => x.MassPoints).ToArray();
        AllSprings = _softBodies.SelectMany(x => x.Springs).ToArray();
        SoftBodiesCrossProduct = _softBodies.GetCrossProduct().ToArray();
    }
}
