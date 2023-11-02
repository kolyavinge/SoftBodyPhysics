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
    private readonly IBordersCalculator _bordersCalculator;

    public SoftBodyBordersUpdater(
        ISoftBodiesCollection softBodiesCollection,
        IBordersCalculator bordersCalculator)
    {
        _softBodiesCollection = softBodiesCollection;
        _bordersCalculator = bordersCalculator;
    }

    public void UpdateBorders()
    {
        foreach (var softBody in _softBodiesCollection.SoftBodies)
        {
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
        softBody.Borders = _bordersCalculator.GetBordersBySegments(softBody.Edges) ??
                           _bordersCalculator.GetBordersByMassPoint(softBody.MassPoints[0].Position);
    }
}
