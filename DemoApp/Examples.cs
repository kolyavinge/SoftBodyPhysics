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
        physicsWorld.Units.TimeDelta = 0.1;

        var softBody = physicsWorld.AddSoftBody();
        var p1 = softBody.AddMassPoint(new(500, 200));
        var p2 = softBody.AddMassPoint(new(500, 400));
        p2.DebugInfo = "A";
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
        var s = softBody.AddSpring(p4, p1);
        s.DebugInfo = "B";
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p4);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(10000, 100));
    }

    public static void SoftBodyCollisionsManyMassPoints(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.TimeDelta = 0.1;
        physicsWorld.Units.SpringStiffness = 10;

        // one

        var softBody = physicsWorld.AddSoftBody();

        var p10 = softBody.AddMassPoint(new(500, 200));
        var p11 = softBody.AddMassPoint(new(500, 250));
        var p12 = softBody.AddMassPoint(new(500, 300));

        var p20 = softBody.AddMassPoint(new(500, 350));
        var p21 = softBody.AddMassPoint(new(550, 350));
        var p22 = softBody.AddMassPoint(new(600, 350));
        var p23 = softBody.AddMassPoint(new(650, 350));

        var p30 = softBody.AddMassPoint(new(700, 350));
        var p31 = softBody.AddMassPoint(new(700, 300));
        var p32 = softBody.AddMassPoint(new(700, 250));
        var p33 = softBody.AddMassPoint(new(700, 200));

        var p40 = softBody.AddMassPoint(new(650, 200));
        var p41 = softBody.AddMassPoint(new(600, 200));
        var p42 = softBody.AddMassPoint(new(550, 200));

        softBody.AddSpring(p10, p11);
        softBody.AddSpring(p11, p12);

        softBody.AddSpring(p12, p20);
        softBody.AddSpring(p20, p21);
        softBody.AddSpring(p21, p22);
        softBody.AddSpring(p22, p23);

        softBody.AddSpring(p23, p30);
        softBody.AddSpring(p30, p31);
        softBody.AddSpring(p31, p32);
        softBody.AddSpring(p32, p33);

        softBody.AddSpring(p33, p40);
        softBody.AddSpring(p40, p41);
        softBody.AddSpring(p41, p42);
        softBody.AddSpring(p42, p10);

        softBody.AddSpring(p10, p30);
        softBody.AddSpring(p20, p33);

        softBody.AddSpring(p21, p42);
        softBody.AddSpring(p22, p41);
        softBody.AddSpring(p23, p40);

        softBody.AddSpring(p11, p32);
        softBody.AddSpring(p12, p31);

        // two

        softBody = physicsWorld.AddSoftBody();

        p10 = softBody.AddMassPoint(new(500 + 100, 1200));
        p11 = softBody.AddMassPoint(new(500 + 100, 1250));
        p12 = softBody.AddMassPoint(new(500 + 100, 1300));

        p20 = softBody.AddMassPoint(new(500 + 100, 1350));
        p21 = softBody.AddMassPoint(new(550 + 100, 1350));
        p22 = softBody.AddMassPoint(new(600 + 100, 1350));
        p23 = softBody.AddMassPoint(new(650 + 100, 1350));

        p30 = softBody.AddMassPoint(new(700 + 100, 1350));
        p31 = softBody.AddMassPoint(new(700 + 100, 1300));
        p32 = softBody.AddMassPoint(new(700 + 100, 1250));
        p33 = softBody.AddMassPoint(new(700 + 100, 1200));

        p40 = softBody.AddMassPoint(new(650 + 100, 1200));
        p41 = softBody.AddMassPoint(new(600 + 100, 1200));
        p42 = softBody.AddMassPoint(new(550 + 100, 1200));

        softBody.AddSpring(p10, p11);
        softBody.AddSpring(p11, p12);

        softBody.AddSpring(p12, p20);
        softBody.AddSpring(p20, p21);
        softBody.AddSpring(p21, p22);
        softBody.AddSpring(p22, p23);

        softBody.AddSpring(p23, p30);
        softBody.AddSpring(p30, p31);
        softBody.AddSpring(p31, p32);
        softBody.AddSpring(p32, p33);

        softBody.AddSpring(p33, p40);
        softBody.AddSpring(p40, p41);
        softBody.AddSpring(p41, p42);
        softBody.AddSpring(p42, p10);

        softBody.AddSpring(p10, p30);
        softBody.AddSpring(p20, p33);

        softBody.AddSpring(p21, p42);
        softBody.AddSpring(p22, p41);
        softBody.AddSpring(p23, p40);

        softBody.AddSpring(p11, p32);
        softBody.AddSpring(p12, p31);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(10000, 100));
    }

    public static void Error1(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.TimeDelta = 0.1;
        physicsWorld.Units.SpringStiffness = 100;

        var softBody = physicsWorld.AddSoftBody();

        var p10 = softBody.AddMassPoint(new(500, 200));
        var p11 = softBody.AddMassPoint(new(500, 250));
        var p12 = softBody.AddMassPoint(new(500, 300));

        var p20 = softBody.AddMassPoint(new(500, 350));
        var p21 = softBody.AddMassPoint(new(550, 350));
        var p22 = softBody.AddMassPoint(new(600, 350));
        var p23 = softBody.AddMassPoint(new(650, 350));

        var p30 = softBody.AddMassPoint(new(700, 350));
        var p31 = softBody.AddMassPoint(new(700, 300));
        var p32 = softBody.AddMassPoint(new(700, 250));
        var p33 = softBody.AddMassPoint(new(700, 200));

        var p40 = softBody.AddMassPoint(new(650, 200));
        var p41 = softBody.AddMassPoint(new(600, 200));
        var p42 = softBody.AddMassPoint(new(550, 200));

        softBody.AddSpring(p10, p11);
        softBody.AddSpring(p11, p12);

        softBody.AddSpring(p12, p20);
        softBody.AddSpring(p20, p21);
        softBody.AddSpring(p21, p22);
        softBody.AddSpring(p22, p23);

        softBody.AddSpring(p23, p30);
        softBody.AddSpring(p30, p31);
        softBody.AddSpring(p31, p32);
        softBody.AddSpring(p32, p33);

        softBody.AddSpring(p33, p40);
        softBody.AddSpring(p40, p41);
        softBody.AddSpring(p41, p42);
        softBody.AddSpring(p42, p10);

        softBody.AddSpring(p10, p30);
        softBody.AddSpring(p20, p33);

        softBody.AddSpring(p21, p42);
        softBody.AddSpring(p22, p41);
        softBody.AddSpring(p23, p40);

        softBody.AddSpring(p11, p32);
        softBody.AddSpring(p12, p31);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(10000, 100));
    }
}
