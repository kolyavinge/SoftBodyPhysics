using System.Globalization;
using System.Windows;
using System.Windows.Media;
using SoftBodyPhysics.Model;

namespace DemoApp;

internal class RenderLogic
{
    private readonly Pen _hardBodyPen = new Pen(Brushes.BlueViolet, 2.0);
    private readonly Pen _prevSpringPen = new Pen(Brushes.Gray, 1.0);
    private readonly Pen _springPen = new Pen(Brushes.CornflowerBlue, 1.0);

    public void OnRender(IPhysicsWorld physicsWorld, DrawingContext dc, double actualHeight, bool showMassPointAddInfo, bool showPrevPositions)
    {
        var yoffset = actualHeight;

        foreach (var hardBody in physicsWorld.HardBodies)
        {
            foreach (var edge in hardBody.Edges)
            {
                dc.DrawLine(_hardBodyPen, new(edge.From.X, yoffset - edge.From.Y), new(edge.To.X, yoffset - edge.To.Y));
            }
        }

        foreach (var softBody in physicsWorld.SoftBodies)
        {
            foreach (var spring in softBody.Springs)
            {
                if (showPrevPositions)
                {
                    var prevPosA = spring.PointA.PrevPosition;
                    var prevPosB = spring.PointB.PrevPosition;
                    dc.DrawLine(_prevSpringPen, new(prevPosA.X, yoffset - prevPosA.Y), new(prevPosB.X, yoffset - prevPosB.Y));
                }

                var posA = spring.PointA.Position;
                var posB = spring.PointB.Position;
                dc.DrawLine(_springPen, new(posA.X, yoffset - posA.Y), new(posB.X, yoffset - posB.Y));
            }

            foreach (var massPoint in softBody.MassPoints)
            {
                if (showPrevPositions)
                {
                    var prevPos = massPoint.PrevPosition;
                    dc.DrawEllipse(Brushes.Gray, null, new(prevPos.X, yoffset - prevPos.Y), massPoint.Radius, massPoint.Radius);
                }

                var pos = massPoint.Position;
                dc.DrawEllipse(Brushes.DarkRed, null, new(pos.X, yoffset - pos.Y), massPoint.Radius, massPoint.Radius);

                if (showMassPointAddInfo)
                {
                    dc.DrawText(
                        new($"{massPoint.Position}\n{massPoint.Velocity}",
                            CultureInfo.CurrentCulture,
                            FlowDirection.LeftToRight,
                            new Typeface(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
                            10.0,
                            Brushes.Green,
                            new NumberSubstitution(),
                            TextFormattingMode.Display,
                            1.0),
                        new(pos.X + 2 * massPoint.Radius, yoffset - pos.Y + 2 * massPoint.Radius));
                }
            }
        }
    }
}
