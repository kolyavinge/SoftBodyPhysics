using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

public enum MassPointState { Normal, Collision }

public interface IMassPoint
{
    Vector Force { get; }

    Vector Velocity { get; }

    Vector Position { get; }

    Vector PrevPosition { get; }

    double Mass { get; set; }

    double Radius { get; set; }

    MassPointState State { get; }

    string? DebugInfo { get; set; }
}

internal class MassPoint : IMassPoint
{
    public Vector Force { get; set; }

    public Vector Velocity { get; set; }

    public Vector Position { get; set; }

    public Vector PrevPosition { get; private set; }

    public double Mass { get; set; }

    public double Radius { get; set; }

    public MassPointState State { get; set; }

    public string? DebugInfo { get; set; }

    public MassPoint(Vector position)
    {
        Position = position;
        PrevPosition = position;
    }

    public void ResetForce()
    {
        Force = Vector.Zero;
    }

    public void ResetState()
    {
        State = MassPointState.Normal;
    }

    public void SavePosition()
    {
        PrevPosition = Position;
    }
}
