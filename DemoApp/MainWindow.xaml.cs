﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Examples;
using SoftBodyPhysics.Core;
using SoftBodyPhysics.Factories;

namespace DemoApp;

public partial class MainWindow : Window
{
    private IPhysicsWorld? _physicsWorld;
    private readonly RenderLogic _renderLogic;
    private readonly DispatcherTimer _timer;
    private uint _frames;

    public MainWindow()
    {
        InitializeComponent();
        InitWorld();
        _renderLogic = new RenderLogic();
        _timer = new DispatcherTimer(DispatcherPriority.Render);
        _timer.Interval = TimeSpan.FromMilliseconds(20);
        _timer.Tick += OnTimer;
    }

    private void InitWorld()
    {
        _frames = 0;
        FramesTextBox.Text = "Frame: 0";
        _physicsWorld = PhysicsWorldFactory.Make();
        Example.ManyBodiesCollisions(_physicsWorld);
    }

    private void UpdateWorld()
    {
        FramesTextBox.Text = $"Frame: {++_frames}";
        _physicsWorld!.Update();
        InvalidateVisual();
    }

    private void OnTimer(object? sender, EventArgs e)
    {
        UpdateWorld();
    }

    protected override void OnRender(DrawingContext dc)
    {
        _renderLogic.OnRender(_physicsWorld!, dc, ActualWidth, ActualHeight, ShowMassPointAddInfo.IsChecked ?? false, ShowGrid.IsChecked ?? false);
    }

    private void OnResetClick(object sender, RoutedEventArgs e)
    {
        InitWorld();
        InvalidateVisual();
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

    private void OnShowMassPointAddInfoChecked(object sender, RoutedEventArgs e)
    {
        InvalidateVisual();
    }

    private void OnShowPrevPositionsChecked(object sender, RoutedEventArgs e)
    {
        InvalidateVisual();
    }

    private void OnShowGridChecked(object sender, RoutedEventArgs e)
    {
        InvalidateVisual();
    }
}
