using System.Collections.Generic;
using SoftBodyPhysics.Calculations;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Intersections;

internal interface ISoftBodyIntersector
{
    IEnumerable<ISoftBody> GetSoftBodyByPoint(Vector point);
}

internal class SoftBodyIntersector : ISoftBodyIntersector
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IPolygonIntersector _polygonIntersector;

    public SoftBodyIntersector(
        ISoftBodiesCollection softBodiesCollection,
        IPolygonIntersector polygonIntersector)
    {
        _softBodiesCollection = softBodiesCollection;
        _polygonIntersector = polygonIntersector;
    }

    public IEnumerable<ISoftBody> GetSoftBodyByPoint(Vector point)
    {
        var softBodies = _softBodiesCollection.SoftBodies;
        for (int i = 0; i < softBodies.Length; i++)
        {
            var softBody = softBodies[i];
            if (_polygonIntersector.IsPointInPolygon(softBody.Edges, softBody.Borders, point))
            {
                yield return softBody;
            }
        }
    }
}
