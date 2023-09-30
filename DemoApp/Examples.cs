using SoftBodyPhysics.Model;

namespace DemoApp;

internal static class Examples
{
    public static void OneMassPoint(IPhysicsWorld physicsWorld)
    {
        var softBody = physicsWorld.AddSoftBody();
        var p = softBody.AddMassPoint(new(400, 250));
        p.Radius = 5;

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(10000, 100));
    }

    public static void OneMassPointCollisions(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.Friction = 0.1;

        var softBody = physicsWorld.AddSoftBody();
        var p = softBody.AddMassPoint(new(150, 1000));
        p.Radius = 5;

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(100, 700), new(200, 600));

        hardBody.AddEdge(new(400, 600), new(300, 600));

        hardBody.AddEdge(new(150, 350), new(250, 450));

        hardBody.AddEdge(new(400, 100), new(500, 200));
        hardBody.AddEdge(new(100, 200), new(200, 100));

        hardBody.AddEdge(new(500, 100), new(500, 1000));
        hardBody.AddEdge(new(100, 100), new(100, 1000));

        hardBody.AddEdge(new(100, 100), new(500, 100));
    }

    public static void OneSoftBody(IPhysicsWorld physicsWorld)
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

    public static void OneSoftBodyCollisions(IPhysicsWorld physicsWorld)
    {
        var softBody = physicsWorld.AddSoftBody();
        var p1 = softBody.AddMassPoint(new(110, 800));
        var p2 = softBody.AddMassPoint(new(180, 800));
        var p3 = softBody.AddMassPoint(new(150, 900));
        softBody.AddSpring(p1, p2);
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p3);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(100, 700), new(200, 600));

        hardBody.AddEdge(new(400, 600), new(300, 600));

        hardBody.AddEdge(new(150, 350), new(250, 450));

        hardBody.AddEdge(new(400, 100), new(500, 200));
        hardBody.AddEdge(new(100, 200), new(200, 100));

        hardBody.AddEdge(new(500, 100), new(500, 1000));
        hardBody.AddEdge(new(100, 100), new(100, 1000));

        hardBody.AddEdge(new(100, 100), new(500, 100));
    }

    public static void OneSoftBodyStay(IPhysicsWorld physicsWorld)
    {
        var square = physicsWorld.AddSoftBody();
        var p1 = square.AddMassPoint(new(400, 100));
        var p2 = square.AddMassPoint(new(400, 200));
        var p3 = square.AddMassPoint(new(600, 200));
        var p4 = square.AddMassPoint(new(600, 100));
        square.AddSpring(p1, p2);
        square.AddSpring(p2, p3);
        square.AddSpring(p3, p4);
        square.AddSpring(p4, p1);
        square.AddSpring(p1, p3);
        square.AddSpring(p2, p4);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(10000, 100));
    }

    public static void OneSoftBodyOneMassPointCollisions(IPhysicsWorld physicsWorld)
    {
        var point = physicsWorld.AddSoftBody();
        point.AddMassPoint(new(500, 400));

        var square = physicsWorld.AddSoftBody();
        var p1 = square.AddMassPoint(new(400, 100));
        var p2 = square.AddMassPoint(new(400, 200));
        var p3 = square.AddMassPoint(new(600, 200));
        var p4 = square.AddMassPoint(new(600, 100));
        square.AddSpring(p1, p2);
        square.AddSpring(p2, p3);
        square.AddSpring(p3, p4);
        square.AddSpring(p4, p1);
        square.AddSpring(p1, p3);
        square.AddSpring(p2, p4);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(10000, 100));
    }

    public static void SoftBodyCollisions(IPhysicsWorld physicsWorld)
    {
        var softBody = physicsWorld.AddSoftBody();
        var p1 = softBody.AddMassPoint(new(500, 200));
        var p2 = softBody.AddMassPoint(new(500, 400));
        var p3 = softBody.AddMassPoint(new(700, 400));
        var p4 = softBody.AddMassPoint(new(700, 200));
        var s = softBody.AddSpring(p1, p2);
        s.IsEdge = true;
        s = softBody.AddSpring(p2, p3);
        s.IsEdge = true;
        s = softBody.AddSpring(p3, p4);
        s.IsEdge = true;
        s = softBody.AddSpring(p4, p1);
        s.IsEdge = true;
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p4);

        softBody = physicsWorld.AddSoftBody();
        p1 = softBody.AddMassPoint(new(400, 1000));
        p2 = softBody.AddMassPoint(new(400, 1200));
        p3 = softBody.AddMassPoint(new(600, 1200));
        p4 = softBody.AddMassPoint(new(600, 1000));
        s = softBody.AddSpring(p1, p2);
        s.IsEdge = true;
        s = softBody.AddSpring(p2, p3);
        s.IsEdge = true;
        s = softBody.AddSpring(p3, p4);
        s.IsEdge = true;
        s = softBody.AddSpring(p4, p1);
        s.IsEdge = true;
        softBody.AddSpring(p1, p3);
        softBody.AddSpring(p2, p4);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(0, 1000));
        hardBody.AddEdge(new(950, 100), new(950, 1000));
        hardBody.AddEdge(new(0, 100), new(950, 100));
    }

    public static void SoftBodyCollisionsManyMassPoints(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.SpringStiffness = 10;

        // one

        var softBody = physicsWorld.AddSoftBody();

        var p10 = softBody.AddMassPoint(new(300, 200));
        var p11 = softBody.AddMassPoint(new(300, 250));
        var p12 = softBody.AddMassPoint(new(300, 300));

        var p20 = softBody.AddMassPoint(new(300, 350));
        var p21 = softBody.AddMassPoint(new(350, 350));
        var p22 = softBody.AddMassPoint(new(400, 350));
        var p23 = softBody.AddMassPoint(new(450, 350));

        var p30 = softBody.AddMassPoint(new(500, 350));
        var p31 = softBody.AddMassPoint(new(500, 300));
        var p32 = softBody.AddMassPoint(new(500, 250));
        var p33 = softBody.AddMassPoint(new(500, 200));

        var p40 = softBody.AddMassPoint(new(450, 200));
        var p41 = softBody.AddMassPoint(new(400, 200));
        var p42 = softBody.AddMassPoint(new(350, 200));

        var s = softBody.AddSpring(p10, p11);
        s.IsEdge = true;
        s = softBody.AddSpring(p11, p12);
        s.IsEdge = true;

        s = softBody.AddSpring(p12, p20);
        s.IsEdge = true;
        s = softBody.AddSpring(p20, p21);
        s.IsEdge = true;
        s = softBody.AddSpring(p21, p22);
        s.IsEdge = true;
        s = softBody.AddSpring(p22, p23);
        s.IsEdge = true;

        s = softBody.AddSpring(p23, p30);
        s.IsEdge = true;
        s = softBody.AddSpring(p30, p31);
        s.IsEdge = true;
        s = softBody.AddSpring(p31, p32);
        s.IsEdge = true;
        s = softBody.AddSpring(p32, p33);
        s.IsEdge = true;

        s = softBody.AddSpring(p33, p40);
        s.IsEdge = true;
        s = softBody.AddSpring(p40, p41);
        s.IsEdge = true;
        s = softBody.AddSpring(p41, p42);
        s.IsEdge = true;
        s = softBody.AddSpring(p42, p10);
        s.IsEdge = true;

        softBody.AddSpring(p10, p30);
        softBody.AddSpring(p20, p33);

        softBody.AddSpring(p21, p42);
        softBody.AddSpring(p22, p41);
        softBody.AddSpring(p23, p40);

        softBody.AddSpring(p11, p32);
        softBody.AddSpring(p12, p31);

        // two

        softBody = physicsWorld.AddSoftBody();

        p10 = softBody.AddMassPoint(new(300 + 120, 1200));
        p11 = softBody.AddMassPoint(new(300 + 120, 1250));
        p12 = softBody.AddMassPoint(new(300 + 120, 1300));

        p20 = softBody.AddMassPoint(new(300 + 120, 1350));
        p21 = softBody.AddMassPoint(new(350 + 120, 1350));
        p22 = softBody.AddMassPoint(new(400 + 120, 1350));
        p23 = softBody.AddMassPoint(new(450 + 120, 1350));

        p30 = softBody.AddMassPoint(new(500 + 120, 1350));
        p31 = softBody.AddMassPoint(new(500 + 120, 1300));
        p32 = softBody.AddMassPoint(new(500 + 120, 1250));
        p33 = softBody.AddMassPoint(new(500 + 120, 1200));

        p40 = softBody.AddMassPoint(new(450 + 120, 1200));
        p41 = softBody.AddMassPoint(new(400 + 120, 1200));
        p42 = softBody.AddMassPoint(new(350 + 120, 1200));

        s = softBody.AddSpring(p10, p11);
        s.IsEdge = true;
        s = softBody.AddSpring(p11, p12);
        s.IsEdge = true;

        s = softBody.AddSpring(p12, p20);
        s.IsEdge = true;
        s = softBody.AddSpring(p20, p21);
        s.IsEdge = true;
        s = softBody.AddSpring(p21, p22);
        s.IsEdge = true;
        s = softBody.AddSpring(p22, p23);
        s.IsEdge = true;

        s = softBody.AddSpring(p23, p30);
        s.IsEdge = true;
        s = softBody.AddSpring(p30, p31);
        s.IsEdge = true;
        s = softBody.AddSpring(p31, p32);
        s.IsEdge = true;
        s = softBody.AddSpring(p32, p33);
        s.IsEdge = true;

        s = softBody.AddSpring(p33, p40);
        s.IsEdge = true;
        s = softBody.AddSpring(p40, p41);
        s.IsEdge = true;
        s = softBody.AddSpring(p41, p42);
        s.IsEdge = true;
        s = softBody.AddSpring(p42, p10);
        s.IsEdge = true;

        softBody.AddSpring(p10, p30);
        softBody.AddSpring(p20, p33);

        softBody.AddSpring(p21, p42);
        softBody.AddSpring(p22, p41);
        softBody.AddSpring(p23, p40);

        softBody.AddSpring(p11, p32);
        softBody.AddSpring(p12, p31);

        var hardBody = physicsWorld.AddHardBody();
        hardBody.AddEdge(new(0, 100), new(0, 1000));
        hardBody.AddEdge(new(1000, 100), new(1000, 1000));
        hardBody.AddEdge(new(0, 100), new(1000, 100));
    }
}
