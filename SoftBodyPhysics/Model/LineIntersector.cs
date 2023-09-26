using SoftBodyPhysics.Utils;

namespace SoftBodyPhysics.Model;

internal interface ILineIntersector
{
    Vector? GetIntersectPoint(Vector line1From, Vector line1To, Vector line2From, Vector line2To);
}

internal class LineIntersector : ILineIntersector
{
    public Vector? GetIntersectPoint(Vector line1From, Vector line1To, Vector line2From, Vector line2To)
    {
        var a1 = line1From.Y - line1To.Y;
        var b1 = line1To.X - line1From.X;
        var c1 = line1From.X * line1To.Y - line1To.X * line1From.Y;

        var a2 = line2From.Y - line2To.Y;
        var b2 = line2To.X - line2From.X;
        var c2 = line2From.X * line2To.Y - line2To.X * line2From.Y;

        var denominator = a1 * b2 - a2 * b1;
        if (denominator == 0) return null;

        var x = (b1 * c2 - b2 * c1) / denominator;
        var y = (a2 * c1 - a1 * c2) / denominator;

        return new(x, y);
    }
}
