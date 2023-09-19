using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

public interface ISoftBody
{
    IReadOnlyCollection<IMassPoint> MassPoints { get; }

    IReadOnlyCollection<ISpring> Springs { get; }

    IMassPoint AddMassPoint(Vector2d position);

    ISpring AddSpring(IMassPoint a, IMassPoint b);
}

internal class SoftBody : ISoftBody
{
    #region ISoftBody
    IReadOnlyCollection<IMassPoint> ISoftBody.MassPoints => MassPoints;
    IReadOnlyCollection<ISpring> ISoftBody.Springs => Springs;
    #endregion

    public List<MassPoint> MassPoints { get; }

    public List<Spring> Springs { get; }

    public SoftBody()
    {
        MassPoints = new List<MassPoint>();
        Springs = new List<Spring>();
    }

    public IMassPoint AddMassPoint(Vector2d position)
    {
        var massPoint = new MassPoint { Position = position };
        MassPoints.Add(massPoint);

        return massPoint;
    }

    public ISpring AddSpring(IMassPoint a, IMassPoint b)
    {
        var spring = new Spring(MassPoints.First(x => x == a), MassPoints.First(x => x == b));
        Springs.Add(spring);

        return spring;
    }
}
