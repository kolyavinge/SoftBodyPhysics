using SoftBodyPhysics.Model;

namespace DemoApp;

internal static class Examples
{
    public static void OneBody(IPhysicsWorld physicsWorld)
    {
        var softBody = physicsWorld.AddSoftBody();
        var p1 = softBody.AddMassPoint(new(300, 300));
        var p2 = softBody.AddMassPoint(new(400, 500));
        var p3 = softBody.AddMassPoint(new(500, 400));
        softBody.AddSpring(p1, p2);
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p3);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(10000, 100));
    }

    public static void HardBodyCollisions(IPhysicsWorld physicsWorld)
    {
        var softBody = physicsWorld.AddSoftBody();
        var p1 = softBody.AddMassPoint(new(100, 300));
        var p2 = softBody.AddMassPoint(new(60, 500));
        var p3 = softBody.AddMassPoint(new(60, 400));
        softBody.AddSpring(p1, p2);
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p3);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 200), new(200, 0));
        hardBody.AddEdge(new(0, 100), new(10000, 100));
        hardBody.AddEdge(new(500, 0), new(1000, 500));
    }

    public static void SoftBodyCollisions(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.TimeDelta = 0.12;

        var softBody = physicsWorld.AddSoftBody();
        var p1 = softBody.AddMassPoint(new(500, 200));
        var p2 = softBody.AddMassPoint(new(500, 400));
        var p3 = softBody.AddMassPoint(new(700, 400));
        var p4 = softBody.AddMassPoint(new(700, 200));
        softBody.AddSpring(p1, p2);
        softBody.AddSpring(p2, p3);
        softBody.AddSpring(p3, p4);
        softBody.AddSpring(p4, p1);
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p4);

        softBody = physicsWorld.AddSoftBody();
        p1 = softBody.AddMassPoint(new(400, 1000));
        p2 = softBody.AddMassPoint(new(400, 1200));
        p3 = softBody.AddMassPoint(new(600, 1200));
        p4 = softBody.AddMassPoint(new(600, 1000));
        softBody.AddSpring(p1, p2);
        softBody.AddSpring(p2, p3);
        softBody.AddSpring(p3, p4);
        softBody.AddSpring(p4, p1);
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p4);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(10000, 100));
    }
}
