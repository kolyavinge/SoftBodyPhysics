using System;
using System.Linq;
using SoftBodyPhysics.Ancillary;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface ISoftBodiesCollection
{
    MassPoint[] MassPoints { get; set; }

    Spring[] Springs { get; set; }

    SoftBody[] SoftBodies { get; }

    SoftBody[] ActivatedSoftBodies { get; }

    int ActivatedSoftBodiesCount { get; }

    void UpdateSoftBodies();

    void UpdateActivatedSoftBodies();
}

internal class SoftBodiesCollection : ISoftBodiesCollection
{
    private readonly IBodyCollisionCollection _bodyCollisionCollection;
    private readonly ISoftBodyBuilder _softBodyBuilder;
    private readonly ISoftBodySpringEdgeDetector _softBodySpringEdgeDetector;

    public MassPoint[] MassPoints { get; set; }

    public Spring[] Springs { get; set; }

    public SoftBody[] SoftBodies { get; private set; }

    public SoftBody[] ActivatedSoftBodies { get; private set; }

    public int ActivatedSoftBodiesCount { get; private set; }

    public SoftBodiesCollection(
        IBodyCollisionCollection bodyCollisionCollection,
        ISoftBodyBuilder softBodyBuilder,
        ISoftBodySpringEdgeDetector softBodySpringEdgeDetector)
    {
        _bodyCollisionCollection = bodyCollisionCollection;
        _softBodyBuilder = softBodyBuilder;
        _softBodySpringEdgeDetector = softBodySpringEdgeDetector;
        MassPoints = Array.Empty<MassPoint>();
        Springs = Array.Empty<Spring>();
        SoftBodies = Array.Empty<SoftBody>();
        ActivatedSoftBodies = Array.Empty<SoftBody>();
    }

    public void UpdateSoftBodies()
    {
        var oldSoftBodies = SoftBodies;
        SoftBodies = _softBodyBuilder.MakeNewSoftBodies(MassPoints, Springs).ToArray();
        _softBodySpringEdgeDetector.DetectEdges(SoftBodies);
        for (int i = 0; i < SoftBodies.Length; i++) SoftBodies[i].Index = i;
        ActivatedSoftBodies = new SoftBody[SoftBodies.Length];
        UpdateActivatedSoftBodies();
        _bodyCollisionCollection.UpdateForSoftBodies(oldSoftBodies, SoftBodies);
    }

    public void UpdateActivatedSoftBodies()
    {
        int count = 0;
        for (int i = 0; i < SoftBodies.Length; i++)
        {
            var softBody = SoftBodies[i];
            if (softBody.IsActive) ActivatedSoftBodies[count++] = SoftBodies[i];
        }
        ActivatedSoftBodiesCount = count;
    }
}
