using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftBodyPhysics.Model;

internal class MassPointsToCheckCollisionsItem
{
    public readonly MassPoint MassPoint;
    public readonly SoftBody Body;

    public MassPointsToCheckCollisionsItem(MassPoint massPoint, SoftBody softBody)
    {
        MassPoint = massPoint;
        Body = softBody;
    }
}

internal interface ISoftBodiesCollection
{
    SoftBody[] SoftBodies { get; }

    MassPoint[] AllMassPoints { get; }

    Spring[] AllSprings { get; }

    MassPointsToCheckCollisionsItem[] MassPointsToCheckCollisions { get; }

    void AddSoftBodies(IEnumerable<SoftBody> softBodies);
}

internal class SoftBodiesCollection : ISoftBodiesCollection
{
    private readonly List<SoftBody> _softBodies;

    public SoftBody[] SoftBodies { get; private set; }

    public MassPoint[] AllMassPoints { get; private set; }

    public Spring[] AllSprings { get; private set; }

    public MassPointsToCheckCollisionsItem[] MassPointsToCheckCollisions { get; private set; }

    public SoftBodiesCollection()
    {
        _softBodies = new List<SoftBody>();
        SoftBodies = Array.Empty<SoftBody>();
        AllMassPoints = Array.Empty<MassPoint>();
        AllSprings = Array.Empty<Spring>();
        MassPointsToCheckCollisions = Array.Empty<MassPointsToCheckCollisionsItem>();
    }

    public void AddSoftBodies(IEnumerable<SoftBody> softBodies)
    {
        _softBodies.AddRange(softBodies);
        SoftBodies = _softBodies.ToArray();
        AllMassPoints = _softBodies.SelectMany(x => x.MassPoints).ToArray();
        AllSprings = _softBodies.SelectMany(x => x.Springs).ToArray();
        MassPointsToCheckCollisions = GetMassPointsToCheckCollisions().ToArray();
    }

    private IEnumerable<MassPointsToCheckCollisionsItem> GetMassPointsToCheckCollisions()
    {
        foreach (var body1 in _softBodies)
        {
            foreach (var body2 in _softBodies)
            {
                if (!object.ReferenceEquals(body1, body2))
                {
                    foreach (var massPoint in body1.MassPoints)
                    {
                        yield return new(massPoint, body2);
                    }
                }
            }
        }
    }
}
