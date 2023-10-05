using Examples;
using SoftBodyPhysics.Factories;

var physicsWorld = PhysicsWorldFactory.Make();

Example.SoftBodyCollisions(physicsWorld);

int frames = 500_000;

for (int i = 0; i < frames; i++)
{
    physicsWorld.Update();
}

Console.WriteLine("done!");
