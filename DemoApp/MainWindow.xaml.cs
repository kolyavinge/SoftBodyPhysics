using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using SoftBodyPhysics.Model;

namespace DemoApp;

public partial class MainWindow : Window
{
    private readonly IPhysicsWorld _physicsWorld;
    private readonly DispatcherTimer _timer;
    private readonly Pen _hardBodyPen = new Pen(Brushes.BlueViolet, 2.0);
    private readonly Pen _springPen = new Pen(Brushes.Gray, 1.0);

    public MainWindow()
    {
        InitializeComponent();
        _physicsWorld = PhysicsWorldFactory.Make();

        OneBody();
        //HardBodyCollisions();
        //SoftBodyCollisions();

        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(20);
        _timer.Tick += OnTimer;
        _timer.Start();
    }

    private void OnTimer(object? sender, EventArgs e)
    {
        _physicsWorld.Update();
        InvalidateVisual();
    }

    private void OneBody()
    {
        var softBody = _physicsWorld.AddSoftBody();
        var p1 = softBody.AddMassPoint(new(300, 300));
        p1.Radius = 5.0;
        var p2 = softBody.AddMassPoint(new(400, 500));
        p2.Radius = 5.0;
        var p3 = softBody.AddMassPoint(new(500, 400));
        p3.Radius = 5.0;
        softBody.AddSpring(p1, p2);
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p3);

        var hardBody = _physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(10000, 100));
    }

    private void HardBodyCollisions()
    {
        var softBody = _physicsWorld.AddSoftBody();
        var p1 = softBody.AddMassPoint(new(100, 300));
        p1.Radius = 5.0;
        var p2 = softBody.AddMassPoint(new(60, 500));
        p2.Radius = 5.0;
        var p3 = softBody.AddMassPoint(new(60, 400));
        p3.Radius = 5.0;
        softBody.AddSpring(p1, p2);
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p3);

        var hardBody = _physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 200), new(200, 0));
        hardBody.AddEdge(new(0, 100), new(10000, 100));
        hardBody.AddEdge(new(500, 0), new(1000, 500));
    }

    private void SoftBodyCollisions()
    {
        var softBody = _physicsWorld.AddSoftBody();
        var p1 = softBody.AddMassPoint(new(500, 200));
        p1.Radius = 5.0;
        var p2 = softBody.AddMassPoint(new(500, 400));
        p2.Radius = 5.0;
        var p3 = softBody.AddMassPoint(new(700, 400));
        p3.Radius = 5.0;
        var p4 = softBody.AddMassPoint(new(700, 200));
        p4.Radius = 5.0;
        softBody.AddSpring(p1, p2);
        softBody.AddSpring(p2, p3);
        softBody.AddSpring(p3, p4);
        softBody.AddSpring(p4, p1);
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p4);

        softBody = _physicsWorld.AddSoftBody();
        p1 = softBody.AddMassPoint(new(400, 1000));
        p1.Radius = 5.0;
        p2 = softBody.AddMassPoint(new(400, 1200));
        p2.Radius = 5.0;
        p3 = softBody.AddMassPoint(new(600, 1200));
        p3.Radius = 5.0;
        p4 = softBody.AddMassPoint(new(600, 1000));
        p4.Radius = 5.0;
        softBody.AddSpring(p1, p2);
        softBody.AddSpring(p2, p3);
        softBody.AddSpring(p3, p4);
        softBody.AddSpring(p4, p1);
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p4);

        var hardBody = _physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(10000, 100));
    }

    protected override void OnRender(DrawingContext dc)
    {
        var yoffset = ActualHeight;

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
