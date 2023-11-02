using System.Collections.Generic;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface ISoftBodyBordersUpdater
{
    void UpdateBorders();
    void UpdateBorders(IEnumerable<SoftBody> softBodies);
    void UpdateBorders(SoftBody softBody);
}

internal class SoftBodyBordersUpdater : ISoftBodyBordersUpdater
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IBordersUpdater _bordersUpdater;

    public SoftBodyBordersUpdater(
        ISoftBodiesCollection softBodiesCollection,
        IBordersUpdater bordersUpdater)
    {
        _softBodiesCollection = softBodiesCollection;
        _bordersUpdater = bordersUpdater;
    }

    public void UpdateBorders()
    {
        var softBodies = _softBodiesCollection.SoftBodies;
        for (var i = 0; i < softBodies.Length; i++)
        {
            var softBody = softBodies[i];
            if (!softBody.IsMoving) continue;
            UpdateBorders(softBody);
        }
    }

    public void UpdateBorders(IEnumerable<SoftBody> softBodies)
    {
        foreach (var softBody in softBodies)
        {
            UpdateBorders(softBody);
        }
    }

    public void UpdateBorders(SoftBody softBody)
    {
        if (softBody.Edges.Length > 0)
        {
            _bordersUpdater.UpdateBordersBySegments(softBody.Borders, softBody.Edges);
        }
        else
        {
            _bordersUpdater.UpdateBordersByMassPoint(softBody.Borders, softBody.MassPoints[0].Position);
        }
    }
}
