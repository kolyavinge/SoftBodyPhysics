using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

public enum CollisionState { Normal, Collision }

public interface IMassPoint
{
    Vector Force { get; }

    Vector Velocity { get; }

    Vector Position { get; }

    Vector PrevPosition { get; }

    double Mass { get; set; }

    CollisionState State { get; }

    string? DebugInfo { get; set; }
}

internal class MassPoint : IMassPoint
{
    public Vector Force { get; set; }

    public Vector Velocity { get; set; }

    public Vector Position { get; set; }

    public Vector PrevPosition { get; private set; }

    public double Mass { get; set; }

    public CollisionState State { get; set; }

    public string? DebugInfo { get; set; }

    public MassPoint(Vector position)
    {
        Position = position;
        PrevPosition = position;
        Velocity = Vector.Zero;
        Force = Vector.Zero;
    }

    public void ResetForce()
    {
        Force = Vector.Zero;
    }

    public void ResetState()
    {
        State = CollisionState.Normal;
    }

    public void SavePosition()
    {
        PrevPosition = Position;
    }
}
