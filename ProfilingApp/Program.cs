using Examples;
using SoftBodyPhysics.Factories;

var physicsWorld = PhysicsWorldFactory.Make();

Example.ManySoftBodyCollisions(physicsWorld);

int frames = 100;

var sw = System.Diagnostics.Stopwatch.StartNew();

for (int i = 0; i < frames; i++)
{
    physicsWorld.Update();
}

sw.Stop();

Console.WriteLine($"Time: {sw.Elapsed}");
Console.ReadKey();
