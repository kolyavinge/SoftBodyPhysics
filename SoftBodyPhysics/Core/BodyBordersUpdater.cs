﻿using System.Collections.Generic;
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
        var softBodies = _softBodiesCollection.ActivatedSoftBodies;
        var count = _softBodiesCollection.ActivatedSoftBodiesCount;
        for (var i = 0; i < count; i++)
        {
            var softBody = softBodies[i];
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
            _bordersUpdater.UpdateBorders(softBody.Borders, softBody.EdgeMassPoints);
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
            _bordersUpdater.UpdateBorders(hardBody.Borders, hardBody.Edges);
        }
    }
}
