using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Ancillary;

internal interface ISoftBodyFinder
{
    void Init(HashSet<MassPoint> massPoints, HashSet<Spring> springs);
    bool Predicate(ISoftBody softBody);
}

internal class SoftBodyFinder : ISoftBodyFinder
{
    private HashSet<MassPoint> _massPoints;
    private HashSet<Spring> _springs;

    public SoftBodyFinder()
    {
        _massPoints = new HashSet<MassPoint>();
        _springs = new HashSet<Spring>();
    }

    public void Init(HashSet<MassPoint> massPoints, HashSet<Spring> springs)
    {
        _massPoints = massPoints;
        _springs = springs;
    }

    public bool Predicate(ISoftBody softBody)
    {
        return
            softBody.MassPoints.Count == _massPoints.Count &&
            softBody.Springs.Count == _springs.Count &&
            softBody.MassPoints.All(_massPoints.Contains) &&
            softBody.Springs.All(_springs.Contains);
    }
}
