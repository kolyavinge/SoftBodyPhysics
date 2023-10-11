using System;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

public interface ISegment
{
    Vector FromPosition { get; }

    Vector ToPosition { get; }
}

public interface ISpring : ISegment
{
    bool IsEdge { get; }

    IMassPoint PointA { get; }

    IMassPoint PointB { get; }

    Vector Force { get; }

    float Stiffness { get; set; }

    float RestLength { get; }

    float DeformLength { get; }

    string? DebugInfo { get; set; }
}

internal class Spring : ISpring
{
    #region ISegment
    Vector ISegment.FromPosition => PointA.Position;
    Vector ISegment.ToPosition => PointB.Position;
    #endregion

    #region ISpring
    bool ISpring.IsEdge => IsEdge;
    IMassPoint ISpring.PointA => PointA;
    IMassPoint ISpring.PointB => PointB;
    Vector ISpring.Force => Force;
    float ISpring.RestLength => RestLength;
    float ISpring.Stiffness { get => Stiffness; set => Stiffness = value; }
    public string? DebugInfo { get; set; }
    #endregion

    // поля для оптимизации

    public bool IsEdge;

    public MassPoint PointA;

    public MassPoint PointB;

    public Vector Force;

    public float Stiffness;

    public float RestLength;

    public float DeformLength => (PointA.Position - PointB.Position).Length - RestLength;

    public Spring(MassPoint a, MassPoint b)
    {
        PointA = a;
        PointB = b;
        RestLength = Math.Abs((a.Position - b.Position).Length);
        Force = Vector.Zero;
    }
}
