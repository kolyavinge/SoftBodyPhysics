using System;
using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Ancillary;

public interface IHardBodyEditor
{
    IHardBody AddHardBody();

    IEdge AddEdge(IHardBody hardBody, Vector from, Vector to);

    void Complete();
}

internal class HardBodyEditor : IHardBodyEditor
{
    private readonly IHardBodyFactory _hardBodyFactory;
    private readonly IEdgeFactory _edgeFactory;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly IBodyBordersUpdater _bodyBordersUpdater;
    private readonly List<HardBody> _newHardBodies;
    private readonly List<(HardBody, Edge)> _newEdges;

    public HardBodyEditor(
        IHardBodyFactory hardBodyFactory,
        IEdgeFactory edgeFactory,
        IHardBodiesCollection hardBodiesCollection,
        IBodyBordersUpdater bodyBordersUpdater)
    {
        _hardBodyFactory = hardBodyFactory;
        _edgeFactory = edgeFactory;
        _hardBodiesCollection = hardBodiesCollection;
        _bodyBordersUpdater = bodyBordersUpdater;
        _newHardBodies = new List<HardBody>();
        _newEdges = new List<(HardBody, Edge)>();
    }

    public IHardBody AddHardBody()
    {
        var hardBody = _hardBodyFactory.Make();
        _newHardBodies.Add(hardBody);

        return hardBody;
    }

    public IEdge AddEdge(IHardBody hardBody, Vector from, Vector to)
    {
        if (hardBody is HardBody hb)
        {
            var edge = _edgeFactory.Make(from, to);
            _newEdges.Add((hb, edge));

            return edge;
        }
        else throw new ArgumentException();
    }

    public void Complete()
    {
        foreach (var group in _newEdges.GroupBy(x => x.Item1, x => x.Item2))
        {
            var hb = group.Key;
            hb.Edges = hb.Edges.Union(group).ToArray();
        }
        _hardBodiesCollection.AddHardBodies(_newHardBodies);
        _bodyBordersUpdater.UpdateBorders(_newHardBodies);
    }
}
