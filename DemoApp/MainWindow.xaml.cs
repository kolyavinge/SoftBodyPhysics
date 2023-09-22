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
        _physicsWorld.Units.MassPointRadius = 3;

        //Examples.OneBody(_physicsWorld);
        //Examples.HardBodyCollisions(_physicsWorld);
        //Examples.Error1(_physicsWorld);
        Examples.SoftBodyCollisions(_physicsWorld);

        _renderLogic = new RenderLogic(_physicsWorld);

        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(20);
        _timer.Tick += OnTimer;
    }

    private void UpdateWorld()
    {
        _physicsWorld.Update();
        InvalidateVisual();
    }

    private void OnTimer(object? sender, EventArgs e)
    {
        UpdateWorld();
    }

    protected override void OnRender(DrawingContext dc)
    {
        _renderLogic.OnRender(dc, ActualHeight, ShowMassPointAddInfo.IsChecked ?? false, ShowPrevPositions.IsChecked ?? false);
    }

    private void OnPlayClick(object sender, RoutedEventArgs e)
    {
        _timer.Start();
    }

    private void OnStopClick(object sender, RoutedEventArgs e)
    {
        _timer.Stop();
    }

    private void OnNextClick(object sender, RoutedEventArgs e)
    {
        _timer.Stop();
        UpdateWorld();
    }
}
