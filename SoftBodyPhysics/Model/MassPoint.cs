using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

public interface IMassPoint
{
    Vector Force { get; }

    Vector Velocity { get; }

    Vector Position { get; set; }

    Vector PrevPosition { get; }

    float Mass { get; set; }

    CollisionState State { get; }

    string? DebugInfo { get; set; }
}

internal class MassPoint : IMassPoint
{
    #region IMassPoint
    Vector IMassPoint.Force => Force;
    Vector IMassPoint.Velocity => Velocity;
    Vector IMassPoint.Position { get => Position; set => Position = value; }
    Vector IMassPoint.PrevPosition => PrevPosition;
    float IMassPoint.Mass { get => Mass; set => Mass = value; }
    CollisionState IMassPoint.State => State;
    public string? DebugInfo { get; set; }
    #endregion

    // поля для оптимизации

    public Vector Force;

    public Vector Velocity;

    public Vector Position;

    public Vector PrevPosition;

    public float Mass;

    public CollisionState State;

    public MassPoint(Vector position)
    {
        Position = position;
        PrevPosition = position;
        Velocity = Vector.Zero;
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
