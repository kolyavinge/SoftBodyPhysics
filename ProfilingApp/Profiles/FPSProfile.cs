using System.Diagnostics;
using Examples;
using SoftBodyPhysics.Factories;

namespace ProfilingApp.Profiles;

internal class FPSProfile
{
    public void Run()
    {
        Update(100);
        Update(250);
        Update(500);
        Update(1000);
        Update(2000);
        Update(4000);
        Update(8000);

        Console.WriteLine("done.");
        Console.ReadKey();
    }

    private void Update(int frames)
    {
        var physicsWorld = PhysicsWorldFactory.Make();

        Example.ManyBodiesCollisions(physicsWorld);

        var sw = Stopwatch.StartNew();
        for (int i = 0; i < frames; i++) physicsWorld.Update();
        sw.Stop();
        Console.WriteLine($"Frames: {frames}\tTime: {sw.Elapsed}\tFPS: {1000 * frames / (int)sw.Elapsed.TotalMilliseconds}");
    }
}
