using System.Globalization;
using System.Windows;
using System.Windows.Media;
using SoftBodyPhysics.Core;

namespace DemoApp;

internal class RenderLogic
{
    private readonly double _massPointRadius = 2.0;
    private readonly Pen _gridPen = new Pen(new SolidColorBrush(new() { A = 255, R = 60, G = 60, B = 60 }), 0.5);
    private readonly Pen _hardBodyPen = new Pen(Brushes.BlueViolet, 2.0);
    private readonly Pen _hardBodyCollisionPen = new Pen(Brushes.Red, 2.0);
    private readonly Pen _springEdgePen = new Pen(Brushes.Blue, 1.0);
    private readonly Pen _springPen = new Pen(Brushes.CornflowerBlue, 1.0);
    private readonly Brush _massPointBrush = Brushes.DarkRed;
    private readonly Brush _massPointCollisionBrush = Brushes.OrangeRed;

    public void OnRender(
        IPhysicsWorld physicsWorld, DrawingContext dc, double actualWidth, double actualHeight, bool showMassPointAddInfo, bool showGrid)
    {
        var yoffset = actualHeight;

        if (showGrid)
        {
            for (double x = 0; x <= actualWidth; x += 100.0)
            {
                dc.DrawLine(_gridPen, new(x, yoffset), new(x, yoffset - actualHeight));
            }

            for (double y = 0; y <= actualHeight; y += 100.0)
            {
                dc.DrawLine(_gridPen, new(0, yoffset - y), new(actualWidth, yoffset - y));
            }
        }

        foreach (var hardBody in physicsWorld.HardBodies)
        {
            var hasCollided = physicsWorld.IsCollidedToAnySoftBody(hardBody);
            var pen = hasCollided ? _hardBodyCollisionPen : _hardBodyPen;
            foreach (var edge in hardBody.Edges)
            {
                dc.DrawLine(pen, new(edge.From.X, yoffset - edge.From.Y), new(edge.To.X, yoffset - edge.To.Y));
            }
        }

        foreach (var softBody in physicsWorld.SoftBodies)
        {
            foreach (var spring in softBody.Springs)
            {
                var posA = spring.PointA.Position;
                var posB = spring.PointB.Position;

                if (spring.IsEdge)
                {
                    dc.DrawLine(_springEdgePen, new(posA.X, yoffset - posA.Y), new(posB.X, yoffset - posB.Y));
                }
                else
                {
                    dc.DrawLine(_springPen, new(posA.X, yoffset - posA.Y), new(posB.X, yoffset - posB.Y));
                }
            }

            var hasCollided = physicsWorld.IsCollidedToAnySoftBody(softBody) || physicsWorld.IsCollidedToAnyHardBody(softBody);
            var brush = hasCollided ? _massPointCollisionBrush : _massPointBrush;

            foreach (var massPoint in softBody.MassPoints)
            {
                var pos = massPoint.Position;
                dc.DrawEllipse(brush, null, new(pos.X, yoffset - pos.Y), _massPointRadius, _massPointRadius);

                if (showMassPointAddInfo)
                {
                    dc.DrawText(
                        new($"{massPoint.Velocity}",
                            CultureInfo.CurrentCulture,
                            FlowDirection.LeftToRight,
                            new Typeface(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
                            10.0,
                            Brushes.Green,
                            new NumberSubstitution(),
                            TextFormattingMode.Display,
                            1.0),
                        new(pos.X + 2 * _massPointRadius, yoffset - pos.Y + 2 * _massPointRadius));
                }
            }
        }
    }
}
