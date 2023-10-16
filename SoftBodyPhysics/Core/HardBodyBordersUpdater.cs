using System;
using System.Collections.Generic;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

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
        foreach (var hardBody in hardBodies)
        {
            hardBody.Borders = _bordersCalculator.GetBordersBySegments(hardBody.Edges) ?? throw new InvalidOperationException();
        }
    }
}
