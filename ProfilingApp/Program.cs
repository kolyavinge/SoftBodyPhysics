using Examples;
using SoftBodyPhysics.Factories;

var physicsWorld = PhysicsWorldFactory.Make();

Example.ManySoftBodyCollisions(physicsWorld);

int frames = 1000;

for (int i = 0; i < frames; i++)
{
    physicsWorld.Update();
}

Console.WriteLine("done!");
