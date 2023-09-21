using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using SoftBodyPhysics.Factories;
using SoftBodyPhysics.Model;

namespace DemoApp;

public partial class MainWindow : Window
{
    private readonly IPhysicsWorld _physicsWorld;
    private readonly RenderLogic _renderLogic;
    private readonly DispatcherTimer _timer;

    public MainWindow()
    {
        InitializeComponent();
        _physicsWorld = PhysicsWorldFactory.Make();
        _physicsWorld.Units.MassPointRadius = 5;

        //Examples.OneBody(_physicsWorld);
        //Examples.HardBodyCollisions(_physicsWorld);
        //Examples.Error1(_physicsWorld);
		Examples.SoftBodyCollisions(_physicsWorld);

        _renderLogic = new RenderLogic(_physicsWorld);

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

    protected override void OnRender(DrawingContext dc)
    {
        _renderLogic.OnRender(dc, ActualHeight);
    }
}
