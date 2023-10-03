using System.Linq;
using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Model;

internal interface ISoftBodyBordersUpdater
{
    void Update();
}

internal class SoftBodyBordersUpdater : ISoftBodyBordersUpdater
{
    private readonly ISoftBodiesCollection _softBodiesCollection;

    public SoftBodyBordersUpdater(ISoftBodiesCollection softBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
    }

    public void Update()
    {
        foreach (var softBody in _softBodiesCollection.SoftBodies)
        {
            var firstEdge = softBody.Edges.FirstOrDefault();
            if (firstEdge is null) continue;

            Vector positionA = firstEdge.PointA.Position;
            Vector positionB;

            double minX = positionA.X;
            double maxX = positionA.X;
            double minY = positionA.Y;
            double maxY = positionA.Y;

            foreach (var edge in softBody.Edges.Skip(1))
            {
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

            softBody.Borders = new(minX, maxX, minY, maxY);
        }
    }
}
