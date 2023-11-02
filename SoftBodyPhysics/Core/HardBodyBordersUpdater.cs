using System.Collections.Generic;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IHardBodyBordersUpdater
{
    void UpdateBorders(IEnumerable<HardBody> hardBodies);
}

internal class HardBodyBordersUpdater : IHardBodyBordersUpdater
{
    private readonly IBordersUpdater _bordersUpdater;

    public HardBodyBordersUpdater(IBordersUpdater bordersUpdater)
    {
        _bordersUpdater = bordersUpdater;
    }

    public void UpdateBorders(IEnumerable<HardBody> hardBodies)
    {
        foreach (var hardBody in hardBodies)
        {
            _bordersUpdater.UpdateBordersBySegments(hardBody.Borders, hardBody.Edges);
        }
    }
}
