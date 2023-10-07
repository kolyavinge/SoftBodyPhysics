using System.Collections.Generic;
using System.Linq;

namespace SoftBodyPhysics.Model;

internal interface IHardBodyBordersUpdater
{
    void UpdateBorders(IEnumerable<HardBody> hardBodies);
}

internal class HardBodyBordersUpdater : IHardBodyBordersUpdater
{
    private readonly IBordersCalculator _bordersCalculator;

    public HardBodyBordersUpdater(IBordersCalculator bordersCalculator)
    {
        _bordersCalculator = bordersCalculator;
    }

    public void UpdateBorders(IEnumerable<HardBody> hardBodies)
    {
        foreach (var hardBody in hardBodies.Where(x => x.Edges.Length > 0))
        {
            hardBody.Borders = _bordersCalculator.GetBorders(hardBody.Edges);
        }
    }
}
