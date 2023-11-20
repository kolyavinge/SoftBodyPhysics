using System;
using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Ancillary;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal class MakeSoftBodiesResult
{
    public readonly List<SoftBody> NewSoftBodies;
    public readonly List<SoftBody> ExistSoftBodies;

    public MakeSoftBodiesResult()
    {
        NewSoftBodies = new List<SoftBody>();
        ExistSoftBodies = new List<SoftBody>();
    }
}

internal interface ISoftBodyBuilder
{
    MakeSoftBodiesResult MakeSoftBodies(SoftBody[] allSoftBodies, MassPoint[] massPoints, Spring[] springs);
}

internal class SoftBodyBuilder : ISoftBodyBuilder
{
    private readonly ISoftBodyFactory _softBodyFactory;
    private readonly ISoftBodyFinder _softBodyFinder;

    public SoftBodyBuilder(
        ISoftBodyFactory softBodyFactory,
        ISoftBodyFinder softBodyFinder)
    {
        _softBodyFactory = softBodyFactory;
        _softBodyFinder = softBodyFinder;
    }

    public MakeSoftBodiesResult MakeSoftBodies(SoftBody[] allSoftBodies, MassPoint[] massPoints, Spring[] springs)
    {
        var result = new MakeSoftBodiesResult();

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
            var bodySprings = springs.Where(x => bodyMassPoints.Contains(x.PointA) && bodyMassPoints.Contains(x.PointB)).ToHashSet();
            _softBodyFinder.Init(bodyMassPoints, bodySprings);
            var existSoftBody = Array.Find(allSoftBodies, _softBodyFinder.Predicate);
            if (existSoftBody is not null)
            {
                result.ExistSoftBodies.Add(existSoftBody);
            }
            else
            {
                var newSoftBody = _softBodyFactory.Make();
                newSoftBody.MassPoints = bodyMassPoints.ToArray();
                newSoftBody.Springs = bodySprings.ToArray();
                result.NewSoftBodies.Add(newSoftBody);
            }
            lookupMassPoints.RemoveAll(bodyMassPoints.Contains);
        }

        return result;
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
