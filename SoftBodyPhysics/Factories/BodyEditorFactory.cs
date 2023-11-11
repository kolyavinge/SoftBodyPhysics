using SoftBodyPhysics.Ancillary;
using SoftBodyPhysics.Core;

namespace SoftBodyPhysics.Factories;

internal interface IBodyEditorFactory
{
    ISoftBodyEditor MakeSoftBodyEditor();
    IHardBodyEditor MakeHardBodyEditor();
}

internal class BodyEditorFactory : IBodyEditorFactory
{
    private readonly IHardBodyFactory _hardBodyFactory;
    private readonly IMassPointFactory _massPointFactory;
    private readonly ISpringFactory _springFactory;
    private readonly IEdgeFactory _edgeFactory;
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly IBodyBordersUpdater _bodyBordersUpdater;
    private readonly ISoftBodySpringEdgeDetector _softBodySpringEdgeDetector;

    public BodyEditorFactory(
        IHardBodyFactory hardBodyFactory,
        IMassPointFactory massPointFactory,
        ISpringFactory springFactory,
        IEdgeFactory edgeFactory,
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        IBodyBordersUpdater bodyBordersUpdater,
        ISoftBodySpringEdgeDetector softBodySpringEdgeDetector)
    {
        _hardBodyFactory = hardBodyFactory;
        _massPointFactory = massPointFactory;
        _springFactory = springFactory;
        _edgeFactory = edgeFactory;
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _bodyBordersUpdater = bodyBordersUpdater;
        _softBodySpringEdgeDetector = softBodySpringEdgeDetector;
    }

    public ISoftBodyEditor MakeSoftBodyEditor()
    {
        return new SoftBodyEditor(
            _massPointFactory,
            _springFactory,
            _softBodiesCollection);
    }

    public IHardBodyEditor MakeHardBodyEditor()
    {
        return new HardBodyEditor(
            _hardBodyFactory,
            _edgeFactory,
            _hardBodiesCollection,
            _bodyBordersUpdater);
    }
}
