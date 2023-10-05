using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Factories;

internal interface IBodyEditorFactory
{
    IBodyEditor Make();
}

internal class BodyEditorFactory : IBodyEditorFactory
{
    private readonly ISoftBodyFactory _softBodyFactory;
    private readonly IHardBodyFactory _hardBodyFactory;
    private readonly IMassPointFactory _massPointFactory;
    private readonly ISpringFactory _springFactory;
    private readonly IEdgeFactory _edgeFactory;
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IHardBodiesCollection _hardBodiesCollection;
    private readonly ISoftBodyBordersUpdater _softBodyBordersUpdater;
    private readonly ISoftBodySpringEdgeDetector _softBodySpringEdgeDetector;

    public BodyEditorFactory(
        ISoftBodyFactory softBodyFactory,
        IHardBodyFactory hardBodyFactory,
        IMassPointFactory massPointFactory,
        ISpringFactory springFactory,
        IEdgeFactory edgeFactory,
        ISoftBodiesCollection softBodiesCollection,
        IHardBodiesCollection hardBodiesCollection,
        ISoftBodyBordersUpdater softBodyBordersUpdater,
        ISoftBodySpringEdgeDetector softBodySpringEdgeDetector)
    {
        _softBodyFactory = softBodyFactory;
        _hardBodyFactory = hardBodyFactory;
        _massPointFactory = massPointFactory;
        _springFactory = springFactory;
        _edgeFactory = edgeFactory;
        _softBodiesCollection = softBodiesCollection;
        _hardBodiesCollection = hardBodiesCollection;
        _softBodyBordersUpdater = softBodyBordersUpdater;
        _softBodySpringEdgeDetector = softBodySpringEdgeDetector;
    }

    public IBodyEditor Make()
    {
        return new BodyEditor(
            _softBodyFactory,
            _hardBodyFactory,
            _massPointFactory,
            _springFactory,
            _edgeFactory,
            _softBodiesCollection,
            _hardBodiesCollection,
            _softBodyBordersUpdater,
            _softBodySpringEdgeDetector);
    }
}
