using System;
using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

public interface ISpring
{
    IMassPoint PointA { get; }

    IMassPoint PointB { get; }

    Vector2d Force { get; }

    double Stiffness { get; set; }

    double RestLength { get; }

    double DeformLength { get; }
}

internal class Spring : ISpring
{
    #region ISpring
    IMassPoint ISpring.PointA => PointA;
    IMassPoint ISpring.PointB => PointB;
    #endregion

    public MassPoint PointA { get; }

    public MassPoint PointB { get; }

    public Vector2d Force { get; set; }

    public double Stiffness { get; set; }

    public double RestLength { get; }

    public double DeformLength => (PointA.Position - PointB.Position).Length - RestLength;

    public Spring(MassPoint a, MassPoint b)
    {
        PointA = a;
        PointB = b;
        Stiffness = Constants.SpringStiffness;
        RestLength = Math.Abs((a.Position - b.Position).Length);
    }
}
