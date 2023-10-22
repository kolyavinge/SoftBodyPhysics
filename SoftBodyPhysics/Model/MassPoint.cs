using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

public interface IMassPoint
{
    Vector Velocity { get; set; }

    Vector Position { get; set; }

    float Mass { get; set; }

    IBarrier? Collision { get; }

    object? Tag { get; set; }
}

internal class MassPoint : IMassPoint
{
    #region IMassPoint
    Vector IMassPoint.Velocity { get => Velocity; set => Velocity = value; }
    Vector IMassPoint.Position { get => Position; set => Position = value; }
    float IMassPoint.Mass { get => Mass; set => Mass = value; }
    IBarrier? IMassPoint.Collision => Collision;
    public object? Tag { get; set; }
    #endregion

    // поля для оптимизации

    public Vector Force;

    public Vector Velocity;

    public Vector Position;

    public Vector PrevPosition;

    public Vector PositionStep;

    public float Mass;

    public IBarrier? Collision;

    public MassPoint(Vector position)
    {
        Position = new(position.x, position.y);
        PrevPosition = new(position.x, position.y);
        Velocity = new Vector(0, 0);
        PositionStep = new Vector(0, 0);
        Force = new Vector(0, 0);
    }
}
