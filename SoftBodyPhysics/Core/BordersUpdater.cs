using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IBordersUpdater
{
    void UpdateBordersBySegments(Borders borders, ISegment[] segments);
    void UpdateBordersByMassPoint(Borders borders, Vector massPointPosition);
}

internal class BordersUpdater : IBordersUpdater
{
    public void UpdateBordersBySegments(Borders borders, ISegment[] segments)
    {
        if (segments.Length == 0) return;

        Vector from = segments[0].FromPosition;
        Vector to = segments[0].ToPosition;

        float minX = from.x;
        float minY = from.y;
        float maxX = to.x;
        float maxY = to.y;

        for (int i = 1; i < segments.Length; i++)
        {
            from = segments[i].FromPosition;
            to = segments[i].ToPosition;

            if (from.x < minX) minX = from.x;
            else if (from.x > maxX) maxX = from.x;

            if (to.x < minX) minX = to.x;
            else if (to.x > maxX) maxX = to.x;

            if (from.y < minY) minY = from.y;
            else if (from.y > maxY) maxY = from.y;

            if (to.y < minY) minY = to.y;
            else if (to.y > maxY) maxY = to.y;
        }

        borders.Set(minX, maxX, minY, maxY);
    }

    public void UpdateBordersByMassPoint(Borders borders, Vector massPointPosition)
    {
        float minX = massPointPosition.x;
        float minY = massPointPosition.y;
        float maxX = massPointPosition.x + Constants.MassPointRadius;
        float maxY = massPointPosition.y + Constants.MassPointRadius;

        borders.Set(minX, maxX, minY, maxY);
    }
}
