using System.Windows.Media;
using SoftBodyPhysics.Model;

namespace DemoApp;

internal class RenderLogic
{
    private readonly Pen _hardBodyPen = new Pen(Brushes.BlueViolet, 2.0);
    private readonly Pen _springPen = new Pen(Brushes.Gray, 1.0);

    private readonly IPhysicsWorld _physicsWorld;

    public RenderLogic(IPhysicsWorld physicsWorld)
    {
        _physicsWorld = physicsWorld;
    }

    public void OnRender(DrawingContext dc, double actualHeight)
    {
        var yoffset = actualHeight;

        foreach (var hardBody in _physicsWorld.HardBodies)
        {
            foreach (var edge in hardBody.Edges)
            {
                dc.DrawLine(_hardBodyPen, new(edge.From.X, yoffset - edge.From.Y), new(edge.To.X, yoffset - edge.To.Y));
            }
        }

        foreach (var softBody in _physicsWorld.SoftBodies)
        {
            foreach (var spring in softBody.Springs)
            {
                var posA = spring.PointA.Position;
                var posB = spring.PointB.Position;
                dc.DrawLine(_springPen, new(posA.X, yoffset - posA.Y), new(posB.X, yoffset - posB.Y));
            }

            foreach (var massPoint in softBody.MassPoints)
            {
                var pos = massPoint.Position;
                dc.DrawEllipse(Brushes.DarkRed, null, new(pos.X, yoffset - pos.Y), massPoint.Radius, massPoint.Radius);
            }
        }
    }
}
