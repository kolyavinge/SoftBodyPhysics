using System;
using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Model;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Ancillary;

public interface ISoftBodyEditor
{
    ISoftBody MakeSoftBody();

    IMassPoint AddMassPoint(ISoftBody softBody, Vector position);

    ISpring AddSpring(ISoftBody softBody, IMassPoint a, IMassPoint b);

    void Complete();
}

internal class SoftBodyEditor : ISoftBodyEditor
{
    private readonly List<Action> _completeActions;
    private readonly ISoftBodyFactory _softBodyFactory;
    private readonly IMassPointFactory _massPointFactory;
    private readonly ISpringFactory _springFactory;
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IBodyBordersUpdater _bodyBordersUpdater;
    private readonly ISoftBodySpringEdgeDetector _softBodySpringEdgeDetector;
    private readonly Dictionary<ISoftBody, SoftBody> _newSoftBodies;
    private readonly Dictionary<IMassPoint, MassPoint> _newMassPoints;

    public SoftBodyEditor(
        ISoftBodyFactory softBodyFactory,
        IMassPointFactory massPointFactory,
        ISpringFactory springFactory,
        ISoftBodiesCollection softBodiesCollection,
        IBodyBordersUpdater bodyBordersUpdater,
        ISoftBodySpringEdgeDetector softBodySpringEdgeDetector)
    {
        _completeActions = new List<Action>();
        _softBodyFactory = softBodyFactory;
        _massPointFactory = massPointFactory;
        _springFactory = springFactory;
        _softBodiesCollection = softBodiesCollection;
        _bodyBordersUpdater = bodyBordersUpdater;
        _softBodySpringEdgeDetector = softBodySpringEdgeDetector;
        _newSoftBodies = new Dictionary<ISoftBody, SoftBody>();
        _newMassPoints = new Dictionary<IMassPoint, MassPoint>();
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

    public void Complete()
    {
        _completeActions.Each(x => x.Invoke());
        _softBodiesCollection.AddSoftBodies(_newSoftBodies.Values);
        _softBodySpringEdgeDetector.DetectEdges(_newSoftBodies.Values);
        _bodyBordersUpdater.UpdateBorders(_newSoftBodies.Values);
    }
}
