namespace SoftBodyPhysics.Model;

internal interface ISoftBodyMovingDetector
{
    void Update();
    void Update(SoftBody softBody);
}

internal class SoftBodyMovingDetector : ISoftBodyMovingDetector
{
    private readonly ISoftBodiesCollection _softBodiesCollection;

    public SoftBodyMovingDetector(
        ISoftBodiesCollection softBodiesCollection)
    {
        _softBodiesCollection = softBodiesCollection;
    }

    public void Update()
    {
        foreach (var softBody in _softBodiesCollection.SoftBodies)
        {
            //softBody.IsMoved = softBody.MassPoints.Any(x => (x.Position - x.PrevPosition).Length > 1.0);
        }
    }

    public void Update(SoftBody softBody)
    {
        //softBody.IsMoved = softBody.MassPoints.Any(x => (x.Position - x.PrevPosition).Length > 0.01);
    }
}
