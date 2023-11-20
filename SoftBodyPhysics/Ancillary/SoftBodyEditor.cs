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
    IMassPoint AddMassPoint(Vector position);

    void DeleteMassPoints(IEnumerable<IMassPoint> massPoints);

    ISpring AddSpring(IMassPoint a, IMassPoint b);

    void DeleteSprings(IEnumerable<ISpring> springs);

    void Complete();
}

internal class SoftBodyEditor : ISoftBodyEditor
{
    private readonly IMassPointFactory _massPointFactory;
    private readonly ISpringFactory _springFactory;
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly List<MassPoint> _newMassPoints;
    private readonly HashSet<MassPoint> _deletedMassPoints;
    private readonly List<Spring> _newSprings;
    private readonly HashSet<Spring> _deletedSprings;

    public SoftBodyEditor(
        IMassPointFactory massPointFactory,
        ISpringFactory springFactory,
        ISoftBodiesCollection softBodiesCollection)
    {
        _massPointFactory = massPointFactory;
        _springFactory = springFactory;
        _softBodiesCollection = softBodiesCollection;
        _newMassPoints = new List<MassPoint>();
        _deletedMassPoints = new HashSet<MassPoint>();
        _newSprings = new List<Spring>();
        _deletedSprings = new HashSet<Spring>();
    }

    public IMassPoint AddMassPoint(Vector position)
    {
        var massPoint = _massPointFactory.Make(position);
        _newMassPoints.Add(massPoint);

        return massPoint;
    }

    public void DeleteMassPoints(IEnumerable<IMassPoint> massPoints)
    {
        if (massPoints.All(x => x is MassPoint))
        {
            foreach (var mp in massPoints)
            {
                _deletedMassPoints.Add((MassPoint)mp);
            }
        }
        else throw new ArgumentException();
    }

    public ISpring AddSpring(IMassPoint a, IMassPoint b)
    {
        if (a is MassPoint mpa && b is MassPoint mpb)
        {
            var spring = _springFactory.Make(mpa, mpb);
            _newSprings.Add(spring);

            return spring;
        }
        else throw new ArgumentException();
    }

    public void DeleteSprings(IEnumerable<ISpring> springs)
    {
        if (springs.All(x => x is Spring))
        {
            foreach (var s in springs)
            {
                _deletedSprings.Add((Spring)s);
            }
        }
        else throw new ArgumentException();
    }

    public void Complete()
    {
        _deletedSprings.AddRange(_softBodiesCollection.Springs.Where(x => _deletedMassPoints.Contains(x.PointA) || _deletedMassPoints.Contains(x.PointB)));

        _softBodiesCollection.MassPoints = _softBodiesCollection.MassPoints.Union(_newMassPoints).Except(_deletedMassPoints).ToArray();
        _softBodiesCollection.Springs = _softBodiesCollection.Springs.Union(_newSprings).Except(_deletedSprings).ToArray();

        _softBodiesCollection.UpdateSoftBodies();
    }
}
