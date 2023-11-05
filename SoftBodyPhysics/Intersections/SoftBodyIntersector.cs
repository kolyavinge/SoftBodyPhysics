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
    private readonly ISegmentIntersector _segmentIntersector;
    private readonly IPolygonIntersector _polygonIntersector;

    public SoftBodyIntersector(
        ISoftBodiesCollection softBodiesCollection,
        ISegmentIntersector segmentIntersector,
        IPolygonIntersector polygonIntersector)
    {
        _softBodiesCollection = softBodiesCollection;
        _segmentIntersector = segmentIntersector;
        _polygonIntersector = polygonIntersector;
    }

    public IEnumerable<ISoftBody> GetSoftBodyByPoint(Vector point)
    {
        var softBodies = _softBodiesCollection.SoftBodies;
        for (int i = 0; i < softBodies.Length; i++)
        {
            var softBody = softBodies[i];
            if (CheckSegmentIntersection(softBody, point))
            {
                yield return softBody;
            }
            else
            {
                if (_polygonIntersector.IsPointInPolygon(softBody.Edges, softBody.Borders, point))
                {
                    yield return softBody;
                }
            }
        }
    }

    private bool CheckSegmentIntersection(SoftBody softBody, Vector point)
    {
        var springs = softBody.Springs;
        for (var j = 0; j < springs.Length; j++)
        {
            var spring = springs[j];
            if (_segmentIntersector.IsIntersected(spring.PointA.Position, spring.PointB.Position, point))
            {
                return true;
            }
        }

        return false;
    }
}
