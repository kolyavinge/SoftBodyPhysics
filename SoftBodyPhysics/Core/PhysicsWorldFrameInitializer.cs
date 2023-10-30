﻿namespace SoftBodyPhysics.Core;

internal interface IPhysicsWorldFrameInitializer
{
    void Init();
}

internal class PhysicsWorldFrameInitializer : IPhysicsWorldFrameInitializer
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;

    public PhysicsWorldFrameInitializer(
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
    }

    public void Init()
    {
        var allMassPoints = _softBodiesCollection.AllMassPoints;
        for (var i = 0; i < allMassPoints.Length; i++)
        {
            var massPoint = allMassPoints[i];
            massPoint.Collision = null;
            massPoint.PositionBeforeUpdate.x = massPoint.Position.x;
            massPoint.PositionBeforeUpdate.y = massPoint.Position.y;
            massPoint.VelocityBeforeUpdate.x = massPoint.Velocity.x;
            massPoint.VelocityBeforeUpdate.y = massPoint.Velocity.y;
        }

        var allEdges = _hardBodiesCollection.AllEdges;
        for (var i = 0; i < allEdges.Length; i++)
        {
            var edge = allEdges[i];
            edge.Collisions.Clear();
        }
    }
}
