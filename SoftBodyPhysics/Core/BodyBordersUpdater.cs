using System.Collections.Generic;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IBodyBordersUpdater
{
    void UpdateBordersAllSoftBodies();
    void UpdateBorders(IEnumerable<SoftBody> softBodies);
    void UpdateBorders(SoftBody softBody);
    void UpdateBorders(IEnumerable<HardBody> hardBodies);
}

internal class BodyBordersUpdater : IBodyBordersUpdater
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IBordersUpdater _bordersUpdater;

    public BodyBordersUpdater(
        ISoftBodiesCollection softBodiesCollection,
        IBordersUpdater bordersUpdater)
    {
        _softBodiesCollection = softBodiesCollection;
        _bordersUpdater = bordersUpdater;
    }

    public void UpdateBordersAllSoftBodies()
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

    public void UpdateBorders(IEnumerable<HardBody> hardBodies)
    {
        foreach (var hardBody in hardBodies)
        {
            _bordersUpdater.UpdateBordersBySegments(hardBody.Borders, hardBody.Edges);
        }
    }
}
