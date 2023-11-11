using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface ISoftBodyBuilder
{
    IEnumerable<SoftBody> MakeNewSoftBodies(MassPoint[] massPoints, Spring[] springs);
}

internal class SoftBodyBuilder : ISoftBodyBuilder
{
    private readonly ISoftBodyFactory _softBodyFactory;

    public SoftBodyBuilder(ISoftBodyFactory softBodyFactory)
    {
        _softBodyFactory = softBodyFactory;
    }

    public IEnumerable<SoftBody> MakeNewSoftBodies(MassPoint[] massPoints, Spring[] springs)
    {
        var springDictionary =
            springs.Select(s => (s.PointA, s.PointB))
            .Union(springs.Select(s => (s.PointB, s.PointA)))
            .GroupBy(x => x.Item1, x => x.Item2)
            .ToDictionary(x => x.Key, x => x.ToList());
        var lookupMassPoints = massPoints.ToList();
        while (lookupMassPoints.Any())
        {
            var bodyMassPoints = new HashSet<MassPoint> { lookupMassPoints[0] };
            FindBodyMassPoints(lookupMassPoints[0], bodyMassPoints, springDictionary);
            var newSoftBody = _softBodyFactory.Make();
            newSoftBody.MassPoints = bodyMassPoints.ToArray();
            newSoftBody.Springs = springs.Where(x => bodyMassPoints.Contains(x.PointA) && bodyMassPoints.Contains(x.PointB)).ToArray();
            yield return newSoftBody;
            lookupMassPoints.RemoveAll(bodyMassPoints.Contains);
        }
    }

    private void FindBodyMassPoints(
        MassPoint parent, HashSet<MassPoint> bodyMassPoints, Dictionary<MassPoint, List<MassPoint>> springDictionary)
    {
        if (!springDictionary.ContainsKey(parent)) return;
        foreach (var child in springDictionary[parent])
        {
            if (!bodyMassPoints.Contains(child))
            {
                bodyMassPoints.Add(child);
                FindBodyMassPoints(child, bodyMassPoints, springDictionary);
            }
        }
    }
}
