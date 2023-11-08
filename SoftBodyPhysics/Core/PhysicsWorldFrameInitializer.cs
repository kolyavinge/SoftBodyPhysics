namespace SoftBodyPhysics.Core;

internal interface IPhysicsWorldFrameInitializer
{
    void Init();
}

internal class PhysicsWorldFrameInitializer : IPhysicsWorldFrameInitializer
{
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IBodyCollisionCollection _bodyCollisionCollection;

    public PhysicsWorldFrameInitializer(
        ISoftBodiesCollection softBodiesCollection,
        IBodyCollisionCollection bodyCollisionCollection)
    {
        _softBodiesCollection = softBodiesCollection;
        _bodyCollisionCollection = bodyCollisionCollection;
    }

    public void Init()
    {
        var allMassPoints = _softBodiesCollection.AllMassPoints;
        for (var i = 0; i < allMassPoints.Length; i++)
        {
            var massPoint = allMassPoints[i];
            massPoint.PositionBeforeUpdate.x = massPoint.Position.x;
            massPoint.PositionBeforeUpdate.y = massPoint.Position.y;
            massPoint.VelocityBeforeUpdate.x = massPoint.Velocity.x;
            massPoint.VelocityBeforeUpdate.y = massPoint.Velocity.y;
        }

        var softBodies = _softBodiesCollection.ActivatedSoftBodies;
        var count = _softBodiesCollection.ActivatedSoftBodiesCount;
        for (var i = 0; i < count; i++)
        {
            var softBody = softBodies[i];
            _bodyCollisionCollection.ResetFor(softBody);
        }
    }
}
