using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Intersections;

internal interface ILineIntersector
{
    Vector? GetIntersectPoint(Vector line1From, Vector line1To, Vector line2From, Vector line2To);
}

internal class LineIntersector : ILineIntersector
{
    public Vector? GetIntersectPoint(Vector line1From, Vector line1To, Vector line2From, Vector line2To)
    {
        var a1 = line1From.y - line1To.y;
        var b1 = line1To.x - line1From.x;
        var c1 = line1From.x * line1To.y - line1To.x * line1From.y;

        var a2 = line2From.y - line2To.y;
        var b2 = line2To.x - line2From.x;
        var c2 = line2From.x * line2To.y - line2To.x * line2From.y;

        var denominator = a1 * b2 - a2 * b1;
        if (denominator == 0) return null;

        var x = (b1 * c2 - b2 * c1) / denominator;
        var y = (a2 * c1 - a1 * c2) / denominator;

        return new(x, y);
    }
}
