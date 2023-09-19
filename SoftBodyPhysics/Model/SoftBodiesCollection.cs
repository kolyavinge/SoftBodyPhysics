using System.Collections.Generic;

namespace SoftBodyPhysics.Model;

internal interface ISoftBodiesCollection
{
    IReadOnlyList<SoftBody> SoftBodies { get; }

    IEnumerable<MassPoint> AllMassPoints { get; }

    IEnumerable<Spring> AllSprings { get; }

    void AddSoftBody(SoftBody softBody);
}

internal class SoftBodiesCollection : ISoftBodiesCollection
{
    private readonly List<SoftBody> _softBodies;

    public IReadOnlyList<SoftBody> SoftBodies => _softBodies;

    public SoftBodiesCollection()
    {
        _softBodies = new List<SoftBody>();
    }

    public IEnumerable<MassPoint> AllMassPoints
    {
        get
        {
            foreach (var body in _softBodies)
            {
                foreach (var mp in body.MassPoints)
                {
                    yield return mp;
                }
            }
        }
    }

    public IEnumerable<Spring> AllSprings
    {
        get
        {
            foreach (var body in _softBodies)
            {
                foreach (var spring in body.Springs)
                {
                    yield return spring;
                }
            }
        }
    }

    public void AddSoftBody(SoftBody softBody)
    {
        _softBodies.Add(softBody);
    }
}
