using SoftBodyPhysics.Geo;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Factories;

internal interface IEdgeFactory
{
    Edge Make(Vector from, Vector to);
}

internal class EdgeFactory : IEdgeFactory
{
    public Edge Make(Vector from, Vector to)
    {
        return new(from, to);
    }
}
