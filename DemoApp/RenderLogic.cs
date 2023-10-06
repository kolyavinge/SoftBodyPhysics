﻿using System.Globalization;
using System.Windows;
using System.Windows.Media;
using SoftBodyPhysics.Model;

namespace DemoApp;

internal class RenderLogic
{
    private readonly double _massPointRadius = 3.0;
    private readonly Pen _gridPen = new Pen(new SolidColorBrush(new() { A = 255, R = 40, G = 40, B = 40 }), 0.5);
    private readonly Pen _hardBodyPen = new Pen(Brushes.BlueViolet, 2.0);
    private readonly Pen _hardBodyCollisionPen = new Pen(Brushes.Red, 2.0);
    private readonly Pen _prevSpringPen = new Pen(Brushes.Gray, 1.0);
    private readonly Pen _springEdgePen = new Pen(Brushes.Blue, 1.0);
    private readonly Pen _springPen = new Pen(Brushes.CornflowerBlue, 1.0);

    public void OnRender(IPhysicsWorld physicsWorld, DrawingContext dc, double actualWidth, double actualHeight, bool showMassPointAddInfo, bool showPrevPositions)
    {
        var yoffset = actualHeight;

        for (double x = 0; x <= actualWidth; x += 100.0)
        {
            dc.DrawLine(_gridPen, new(x, yoffset), new(x, yoffset - actualHeight));
        }

        for (double y = 0; y <= actualHeight; y += 100.0)
        {
            dc.DrawLine(_gridPen, new(0, yoffset - y), new(actualWidth, yoffset - y));
        }

        foreach (var hardBody in physicsWorld.HardBodies)
        {
            foreach (var edge in hardBody.Edges)
            {
                if (edge.State == CollisionState.Normal)
                {
                    dc.DrawLine(_hardBodyPen, new(edge.From.X, yoffset - edge.From.Y), new(edge.To.X, yoffset - edge.To.Y));
                }
                else
                {
                    dc.DrawLine(_hardBodyCollisionPen, new(edge.From.X, yoffset - edge.From.Y), new(edge.To.X, yoffset - edge.To.Y));
                }
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

                if (spring.IsEdge)
                {
                    dc.DrawLine(_springEdgePen, new(posA.X, yoffset - posA.Y), new(posB.X, yoffset - posB.Y));
                }
                else
                {
                    dc.DrawLine(_springPen, new(posA.X, yoffset - posA.Y), new(posB.X, yoffset - posB.Y));
                }
            }

            foreach (var massPoint in softBody.MassPoints)
            {
                if (showPrevPositions)
                {
                    var prevPos = massPoint.PrevPosition;
                    dc.DrawEllipse(Brushes.Gray, null, new(prevPos.X, yoffset - prevPos.Y), _massPointRadius, _massPointRadius);
                }

                var pos = massPoint.Position;
                if (massPoint.State == CollisionState.Normal)
                {
                    dc.DrawEllipse(Brushes.DarkRed, null, new(pos.X, yoffset - pos.Y), _massPointRadius, _massPointRadius);
                }
                else if (massPoint.State == CollisionState.Collision)
                {
                    dc.DrawEllipse(Brushes.OrangeRed, null, new(pos.X, yoffset - pos.Y), _massPointRadius, _massPointRadius);
                }

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
                        new(pos.X + 2 * _massPointRadius, yoffset - pos.Y + 2 * _massPointRadius));
                }
            }
        }
    }
}
