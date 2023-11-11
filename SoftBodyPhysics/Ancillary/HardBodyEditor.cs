using System;
using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Model;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Ancillary;

public interface IHardBodyEditor
{
    IHardBody AddHardBody();

    IEdge AddEdge(IHardBody hardBody, Vector from, Vector to);

    void Complete();
}

internal class HardBodyEditor : IHardBodyEditor
{
    private readonly List<Action> _completeActions;
    private readonly IHardBodyFactory _hardBodyFactory;
    private readonly IEdgeFactory _edgeFactory;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly IBodyBordersUpdater _bodyBordersUpdater;
    private readonly Dictionary<IHardBody, HardBody> _newHardBodies;

    public HardBodyEditor(
        IHardBodyFactory hardBodyFactory,
        IEdgeFactory edgeFactory,
        IHardBodiesCollection hardBodiesCollection,
        IBodyBordersUpdater bodyBordersUpdater)
    {
        _completeActions = new List<Action>();
        _hardBodyFactory = hardBodyFactory;
        _edgeFactory = edgeFactory;
        _hardBodiesCollection = hardBodiesCollection;
        _bodyBordersUpdater = bodyBordersUpdater;
        _newHardBodies = new Dictionary<IHardBody, HardBody>();
    }

    public IHardBody AddHardBody()
    {
        var hardBody = _hardBodyFactory.Make();
        _newHardBodies.Add(hardBody, hardBody);

        return hardBody;
    }

    public IEdge AddEdge(IHardBody hardBody, Vector from, Vector to)
    {
        var edge = _edgeFactory.Make(from, to);
        Action action = () =>
        {
            var h = _newHardBodies[hardBody];
            h.Edges = h.Edges.Union(new[] { edge }).ToArray();
        };

        _completeActions.Add(action);

        return edge;
    }

    public void Complete()
    {
        _completeActions.Each(x => x.Invoke());
        _hardBodiesCollection.AddHardBodies(_newHardBodies.Values);
        _bodyBordersUpdater.UpdateBorders(_newHardBodies.Values);
    }
}
