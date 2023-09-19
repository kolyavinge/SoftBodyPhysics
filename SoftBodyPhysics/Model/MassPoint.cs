using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

public interface IMassPoint
{
    Vector2d Force { get; }

    Vector2d Velocity { get; }

    Vector2d Position { get; }

    Vector2d PrevPosition { get; }

    double Mass { get; set; }

    double Radius { get; set; }
}

internal class MassPoint : IMassPoint
{
    public Vector2d Force { get; set; }

    public Vector2d Velocity { get; set; }

    public Vector2d Position { get; set; }

    public Vector2d PrevPosition { get; private set; }

    public double Mass { get; set; }

    public double Radius { get; set; }

    public MassPoint()
    {
        Mass = Constants.Mass;
        Radius = Constants.MassPointRadius;
    }

    public void ResetForce()
    {
        Force = Vector2d.Zero;
    }

    public void SavePosition()
    {
        PrevPosition = Position;
    }
}
