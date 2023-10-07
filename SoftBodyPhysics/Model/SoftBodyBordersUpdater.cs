using System.Collections.Generic;
using System.Linq;

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
        foreach (var softBody in softBodies.Where(x => x.SpringsToCheckCollisions.Length > 0))
        {
            softBody.Borders = _bordersCalculator.GetBorders(softBody.SpringsToCheckCollisions);
        }
    }
}
