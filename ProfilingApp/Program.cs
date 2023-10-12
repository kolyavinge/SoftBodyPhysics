using Examples;
using SoftBodyPhysics.Factories;

var physicsWorld = PhysicsWorldFactory.Make();

Example.ManyBodiesCollisions(physicsWorld);

int frames = 4000;

var sw = System.Diagnostics.Stopwatch.StartNew();

for (int i = 0; i < frames; i++)
{
    physicsWorld.Update();
}

sw.Stop();

Console.WriteLine($"Time: {sw.Elapsed}");
Console.WriteLine($"{frames / sw.Elapsed.Seconds} frames per second");
Console.ReadKey();
