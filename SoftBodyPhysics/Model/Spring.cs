using System.Collections.Generic;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

public interface ISegment
{
    Vector FromPosition { get; }

    Vector ToPosition { get; }
}

public interface ISpring : ISegment, IBarrier
{
    bool IsEdge { get; }

    IMassPoint PointA { get; }

    IMassPoint PointB { get; }

    Vector Force { get; }

    float Stiffness { get; set; }

    float RestLength { get; }

    IReadOnlyCollection<IMassPoint> Collisions { get; }

    object? Tag { get; set; }
}

internal class Spring : ISpring
{
    #region ISpring
    Vector ISegment.FromPosition => PointA.Position;
    Vector ISegment.ToPosition => PointB.Position;
    bool ISpring.IsEdge => IsEdge;
    IMassPoint ISpring.PointA => PointA;
    IMassPoint ISpring.PointB => PointB;
    Vector ISpring.Force => Force;
    float ISpring.RestLength => RestLength;
    float ISpring.Stiffness { get => Stiffness; set => Stiffness = value; }
    IReadOnlyCollection<IMassPoint> ISpring.Collisions => Collisions;
    public object? Tag { get; set; }
    #endregion

    // поля для оптимизации

    public bool IsEdge;

    public readonly MassPoint PointA;

    public readonly MassPoint PointB;

    public Vector Force;

    public float Stiffness;

    public readonly float RestLength;

    public readonly List<IMassPoint> Collisions;

    public Spring(MassPoint a, MassPoint b)
    {
        PointA = a;
        PointB = b;
        RestLength = Vector.GetDistanceBetween(a.Position, b.Position);
        Force = new Vector(0, 0);
        Collisions = new List<IMassPoint>();
    }
}
