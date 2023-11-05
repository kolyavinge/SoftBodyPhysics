using System;
using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal class SoftBodyPair
{
    public readonly SoftBody Body1;
    public readonly SoftBody Body2;

    public SoftBodyPair(SoftBody body1, SoftBody body2)
    {
        Body1 = body1;
        Body2 = body2;
    }
}

internal interface ISoftBodiesCollection
{
    SoftBody[] SoftBodies { get; }

    SoftBody[] ActivatedSoftBodies { get; }

    int ActivatedSoftBodiesCount { get; }

    MassPoint[] AllMassPoints { get; }

    Spring[] AllSprings { get; }

    void AddSoftBodies(IEnumerable<SoftBody> softBodies);

    void UpdateActivatedSoftBodies();
}

internal class SoftBodiesCollection : ISoftBodiesCollection
{
    private readonly List<SoftBody> _softBodies;

    public SoftBody[] SoftBodies { get; private set; }

    public SoftBody[] ActivatedSoftBodies { get; private set; }

    public int ActivatedSoftBodiesCount { get; private set; }

    public MassPoint[] AllMassPoints { get; private set; }

    public Spring[] AllSprings { get; private set; }

    public SoftBodiesCollection()
    {
        _softBodies = new List<SoftBody>();
        SoftBodies = Array.Empty<SoftBody>();
        ActivatedSoftBodies = Array.Empty<SoftBody>();
        AllMassPoints = Array.Empty<MassPoint>();
        AllSprings = Array.Empty<Spring>();
    }

    public void AddSoftBodies(IEnumerable<SoftBody> softBodies)
    {
        _softBodies.AddRange(softBodies);
        SoftBodies = _softBodies.ToArray();
        AllMassPoints = _softBodies.SelectMany(x => x.MassPoints).ToArray();
        AllSprings = _softBodies.SelectMany(x => x.Springs).ToArray();
        ActivatedSoftBodies = new SoftBody[_softBodies.Count];
        UpdateActivatedSoftBodies();
    }

    public void UpdateActivatedSoftBodies()
    {
        int count = 0;
        for (int i = 0; i < _softBodies.Count; i++)
        {
            var softBody = _softBodies[i];
            if (softBody.IsActive)
            {
                ActivatedSoftBodies[count++] = _softBodies[i];
            }
        }
        ActivatedSoftBodiesCount = count;
    }
}
