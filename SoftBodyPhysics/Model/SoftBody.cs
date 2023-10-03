using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

public interface ISoftBody
{
    IReadOnlyCollection<IMassPoint> MassPoints { get; }

    IReadOnlyCollection<ISpring> Springs { get; }

    IMassPoint AddMassPoint(Vector position);

    ISpring AddSpring(IMassPoint a, IMassPoint b);
}

internal class SoftBody : ISoftBody
{
    private readonly IPhysicsUnits _physicsUnits;

    #region ISoftBody
    IReadOnlyCollection<IMassPoint> ISoftBody.MassPoints => MassPoints;
    IReadOnlyCollection<ISpring> ISoftBody.Springs => Springs;
    #endregion

    public readonly List<MassPoint> MassPoints;

    public readonly List<Spring> Springs;

    public List<Spring> Edges => Springs.Where(x => x.IsEdge).ToList();

    public Borders Borders;

    public SoftBody(IPhysicsUnits physicsUnits)
    {
        _physicsUnits = physicsUnits;
        MassPoints = new List<MassPoint>();
        Springs = new List<Spring>();
    }

    public IMassPoint AddMassPoint(Vector position)
    {
        var massPoint = new MassPoint(position)
        {
            Mass = _physicsUnits.Mass,
            Radius = _physicsUnits.MassPointRadius
        };
        MassPoints.Add(massPoint);

        return massPoint;
    }

    public ISpring AddSpring(IMassPoint a, IMassPoint b)
    {
        var spring = new Spring(MassPoints.First(x => x == a), MassPoints.First(x => x == b))
        {
            Stiffness = _physicsUnits.SpringStiffness
        };
        Springs.Add(spring);

        return spring;
    }
}
