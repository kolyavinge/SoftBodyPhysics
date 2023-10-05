using System.Collections.Generic;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

internal interface IBordersCalculator
{
    Borders GetBorders(IReadOnlyList<Spring> springs);
}

internal class BordersCalculator : IBordersCalculator
{
    public Borders GetBorders(IReadOnlyList<Spring> springs)
    {
        var firstSpring = springs[0];

        Vector positionA = firstSpring.PointA.Position;
        Vector positionB;

        double minX = positionA.X;
        double maxX = positionA.X;
        double minY = positionA.Y;
        double maxY = positionA.Y;

        for (int i = 1; i < springs.Count; i++)
        {
            var edge = springs[i];

            positionA = edge.PointA.Position;
            positionB = edge.PointB.Position;

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
}
