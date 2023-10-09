using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

internal interface IBordersCalculator
{
    Borders? GetBordersBySegments(IReadOnlyList<ISegment> segments);
    Borders GetBordersByMassPoint(Vector massPointPosition);
}

internal class BordersCalculator : IBordersCalculator
{
    public Borders? GetBordersBySegments(IReadOnlyList<ISegment> segments)
    {
        if (!segments.Any()) return null;

        var first = segments[0];

        Vector positionA = first.FromPosition;
        Vector positionB = first.ToPosition;

        double minX = positionA.X;
        double minY = positionA.Y;
        double maxX = positionB.X;
        double maxY = positionB.Y;

        for (int i = 1; i < segments.Count; i++)
        {
            var edge = segments[i];

            positionA = edge.FromPosition;
            positionB = edge.ToPosition;

            if (positionA.X < minX) minX = positionA.X;
            if (positionB.X < minX) minX = positionB.X;

            if (positionA.X > maxX) maxX = positionA.X;
            if (positionB.X > maxX) maxX = positionB.X;

            if (positionA.Y < minY) minY = positionA.Y;
            if (positionB.Y < minY) minY = positionB.Y;

            if (positionA.Y > maxY) maxY = positionA.Y;
            if (positionB.Y > maxY) maxY = positionB.Y;
        }

        return new(minX, maxX, minY, maxY);
    }

    public Borders GetBordersByMassPoint(Vector massPointPosition)
    {
        double minX = massPointPosition.X;
        double minY = massPointPosition.Y;
        double maxX = massPointPosition.X + Constants.MassPointRadius;
        double maxY = massPointPosition.Y + Constants.MassPointRadius;

        return new(minX, maxX, minY, maxY);
    }
}
