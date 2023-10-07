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

    double Stiffness { get; set; }

    double RestLength { get; }

    double DeformLength { get; }

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
    double ISpring.RestLength => RestLength;
    double ISpring.Stiffness { get => Stiffness; set => Stiffness = value; }
    public string? DebugInfo { get; set; }
    #endregion

    // поля для оптимизации

    public bool IsEdge;

    public MassPoint PointA;

    public MassPoint PointB;

    public Vector Force;

    public double Stiffness;

    public double RestLength;

    public double DeformLength => (PointA.Position - PointB.Position).Length - RestLength;

    public Spring(MassPoint a, MassPoint b)
    {
        PointA = a;
        PointB = b;
        RestLength = Math.Abs((a.Position - b.Position).Length);
        Force = Vector.Zero;
    }
}
