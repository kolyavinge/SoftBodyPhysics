using System.Collections.Generic;

namespace SoftBodyPhysics.Model;

internal interface ISoftBodyBordersUpdater
{
    void UpdateBorders(IEnumerable<SoftBody> softBodies);
}

internal class SoftBodyBordersUpdater : ISoftBodyBordersUpdater
{
    private readonly IBordersCalculator _bordersCalculator;

    public SoftBodyBordersUpdater(IBordersCalculator bordersCalculator)
    {
        _bordersCalculator = bordersCalculator;
    }

    public void UpdateBorders(IEnumerable<SoftBody> softBodies)
    {
        foreach (var softBody in softBodies)
        {
            softBody.Borders = _bordersCalculator.GetBordersBySegments(softBody.SpringsToCheckCollisions)
                               ?? _bordersCalculator.GetBordersByMassPoint(softBody.MassPoints[0].Position);
        }
    }
}
