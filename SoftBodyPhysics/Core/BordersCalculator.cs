using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IBordersCalculator
{
    Borders? GetBordersBySegments(ISegment[] segments);
    Borders GetBordersByMassPoint(Vector massPointPosition);
}

internal class BordersCalculator : IBordersCalculator
{
    public Borders? GetBordersBySegments(ISegment[] segments)
    {
        if (segments.Length == 0) return null;

        var first = segments[0];

        Vector positionA = first.FromPosition;
        Vector positionB = first.ToPosition;

        float minX = positionA.x;
        float minY = positionA.y;
        float maxX = positionB.x;
        float maxY = positionB.y;

        for (int i = 1; i < segments.Length; i++)
        {
            var edge = segments[i];

            positionA = edge.FromPosition;
            positionB = edge.ToPosition;

            if (positionA.x < minX) minX = positionA.x;
            if (positionB.x < minX) minX = positionB.x;

            if (positionA.x > maxX) maxX = positionA.x;
            if (positionB.x > maxX) maxX = positionB.x;

            if (positionA.y < minY) minY = positionA.y;
            if (positionB.y < minY) minY = positionB.y;

            if (positionA.y > maxY) maxY = positionA.y;
            if (positionB.y > maxY) maxY = positionB.y;
        }

        return new(minX, maxX, minY, maxY);
    }

    public Borders GetBordersByMassPoint(Vector massPointPosition)
    {
        float minX = massPointPosition.x;
        float minY = massPointPosition.y;
        float maxX = massPointPosition.x + Constants.MassPointRadius;
        float maxY = massPointPosition.y + Constants.MassPointRadius;

        return new(minX, maxX, minY, maxY);
    }
}
