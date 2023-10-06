using System;
using System.Collections.Generic;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Utils;
using System.Linq;

namespace SoftBodyPhysics.Model;

public interface IBodyEditor
{
    ISoftBody MakeSoftBody();

    IMassPoint AddMassPoint(ISoftBody softBody, Vector position);

    ISpring AddSpring(ISoftBody softBody, IMassPoint a, IMassPoint b);

    IHardBody AddHardBody();

    IEdge AddEdge(IHardBody hardBody, Vector from, Vector to);

    void Complete();
}

internal class BodyEditor : IBodyEditor
{
    private readonly List<Action> _completeActions;
    private readonly ISoftBodyFactory _softBodyFactory;
    private readonly IHardBodyFactory _hardBodyFactory;
    private readonly IMassPointFactory _massPointFactory;
    private readonly ISpringFactory _springFactory;
    private readonly IEdgeFactory _edgeFactory;
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly ISoftBodyBordersUpdater _softBodyBordersUpdater;
    private readonly ISoftBodySpringEdgeDetector _softBodySpringEdgeDetector;
    private readonly Dictionary<ISoftBody, SoftBody> _newSoftBodies;
    private readonly Dictionary<IMassPoint, MassPoint> _newMassPoints;
    private readonly Dictionary<IHardBody, HardBody> _newHardBodies;

    public BodyEditor(
        ISoftBodyFactory softBodyFactory,
        IHardBodyFactory hardBodyFactory,
        IMassPointFactory massPointFactory,
        ISpringFactory springFactory,
        IEdgeFactory edgeFactory,
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        ISoftBodyBordersUpdater softBodyBordersUpdater,
        ISoftBodySpringEdgeDetector softBodySpringEdgeDetector)
    {
        _completeActions = new List<Action>();
        _softBodyFactory = softBodyFactory;
        _hardBodyFactory = hardBodyFactory;
        _massPointFactory = massPointFactory;
        _springFactory = springFactory;
        _edgeFactory = edgeFactory;
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _softBodyBordersUpdater = softBodyBordersUpdater;
        _softBodySpringEdgeDetector = softBodySpringEdgeDetector;
        _newSoftBodies = new Dictionary<ISoftBody, SoftBody>();
        _newMassPoints = new Dictionary<IMassPoint, MassPoint>();
        _newHardBodies = new Dictionary<IHardBody, HardBody>();
    }

    public ISoftBody MakeSoftBody()
    {
        var softBody = _softBodyFactory.Make();
        _newSoftBodies.Add(softBody, softBody);

        return softBody;
    }

    public IMassPoint AddMassPoint(ISoftBody softBody, Vector position)
    {
        var massPoint = _massPointFactory.Make(position);
        _newMassPoints.Add(massPoint, massPoint);

        Action action = () =>
        {
            var s = _newSoftBodies[softBody];
            s.MassPoints = s.MassPoints.Union(new[] { massPoint }).ToArray();
        };

        _completeActions.Add(action);

        return massPoint;
    }

    public ISpring AddSpring(ISoftBody softBody, IMassPoint a, IMassPoint b)
    {
        var ma = _newMassPoints[a];
        var mb = _newMassPoints[b];
        var spring = _springFactory.Make(ma, mb);

        Action action = () =>
        {
            var s = _newSoftBodies[softBody];
            s.Springs = s.Springs.Union(new[] { spring }).ToArray();
        };

        _completeActions.Add(action);

        return spring;
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
            h.Edges.Add(edge);
        };

        _completeActions.Add(action);

        return edge;
    }

    public void Complete()
    {
        _completeActions.Each(x => x.Invoke());
        _softBodiesCollection.AddSoftBodies(_newSoftBodies.Values);
        _hardBodiesCollection.AddHardBodies(_newHardBodies.Values);
        _softBodySpringEdgeDetector.DetectEdges(_newSoftBodies.Values);
        _softBodyBordersUpdater.UpdateBorders(_newSoftBodies.Values);
    }
}
