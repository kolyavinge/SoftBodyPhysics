using System.Diagnostics;
using Examples;
using SoftBodyPhysics.Factories;

namespace ProfilingApp.Profiles;

internal class MaxTimeProfile
{
    public void Run()
    {
        Update(8000, 4);

        Console.WriteLine("done.");
        Console.ReadKey();
    }

    private void Update(int frames, double maxTime)
    {
        var physicsWorld = PhysicsWorldFactory.Make();

        Example.ManyBodiesCollisions(physicsWorld);

        var result = new List<TimeResult>();
        for (int i = 0; i < frames; i++)
        {
            var sw = Stopwatch.StartNew();
            physicsWorld.Update();
            sw.Stop();
            if (sw.Elapsed.TotalMilliseconds > maxTime)
            {
                result.Add(new() { Frame = i, Time = sw.Elapsed.TotalMilliseconds });
            }
        }

        result = result.OrderByDescending(x => x.Time).ToList();
        if (result.Count > 100) result = result.Take(100).ToList();
        foreach (var item in result)
        {
            Console.WriteLine($"Time: {item.Time:F5}\tFrame: {item.Frame}");
        }
    }

    struct TimeResult
    {
        public int Frame;
        public double Time;
    }
}
