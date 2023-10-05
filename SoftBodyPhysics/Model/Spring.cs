using System;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

public interface ISegment
{
    IMassPoint PointA { get; }

    IMassPoint PointB { get; }
}

public interface ISpring : ISegment
{
    bool IsEdge { get; }

    Vector Force { get; }

    double Stiffness { get; set; }

    double RestLength { get; }

    double DeformLength { get; }

    string? DebugInfo { get; set; }
}

internal class Spring : ISpring
{
    #region ISegment
    IMassPoint ISegment.PointA => PointA;
    IMassPoint ISegment.PointB => PointB;
    #endregion

    public bool IsEdge { get; set; }

    public MassPoint PointA { get; }

    public MassPoint PointB { get; }

    public Vector Force { get; set; }

    public double Stiffness { get; set; }

    public double RestLength { get; }

    public double DeformLength => (PointA.Position - PointB.Position).Length - RestLength;

    public string? DebugInfo { get; set; }

    public Spring(MassPoint a, MassPoint b)
    {
        PointA = a;
        PointB = b;
        RestLength = Math.Abs((a.Position - b.Position).Length);
    }
}
