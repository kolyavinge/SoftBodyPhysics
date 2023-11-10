using System.Collections.Generic;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IBodyBordersUpdater
{
    void UpdateBorders(SoftBody softBody);
    void UpdateBorders(IEnumerable<SoftBody> softBodies);
    void UpdateBorders(IEnumerable<HardBody> hardBodies);
}

internal class BodyBordersUpdater : IBodyBordersUpdater
{
    private readonly IBordersUpdater _bordersUpdater;

    public BodyBordersUpdater(
        IBordersUpdater bordersUpdater)
    {
        _bordersUpdater = bordersUpdater;
    }

    public void UpdateBorders(SoftBody softBody)
    {
        if (softBody.Edges.Length > 0)
        {
            _bordersUpdater.UpdateBorders(softBody.Borders, softBody.EdgeMassPoints);
        }
        else
        {
            _bordersUpdater.UpdateBordersByMassPoint(softBody.Borders, softBody.MassPoints[0].Position);
        }
    }

    public void UpdateBorders(IEnumerable<SoftBody> softBodies)
    {
        foreach (var softBody in softBodies)
        {
            UpdateBorders(softBody);
        }
    }

    public void UpdateBorders(IEnumerable<HardBody> hardBodies)
    {
        foreach (var hardBody in hardBodies)
        {
            _bordersUpdater.UpdateBorders(hardBody.Borders, hardBody.Edges);
        }
    }
}
