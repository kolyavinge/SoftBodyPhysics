using SoftBodyPhysics.Model;

namespace Examples;

public static class Example
{
    public static void OneMassPointCollisions(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.Friction = 0.1;

        var editor = physicsWorld.MakEditor();

        var softBody = editor.MakeSoftBody();
        editor.AddMassPoint(softBody, new(150, 1000));

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 700), new(200, 600));

        editor.AddEdge(hardBody, new(400, 600), new(300, 600));

        editor.AddEdge(hardBody, new(150, 350), new(250, 450));

        editor.AddEdge(hardBody, new(400, 100), new(500, 200));
        editor.AddEdge(hardBody, new(100, 200), new(200, 100));

        editor.AddEdge(hardBody, new(500, 100), new(500, 1000));
        editor.AddEdge(hardBody, new(100, 100), new(100, 1000));

        editor.AddEdge(hardBody, new(100, 100), new(500, 100));

        editor.Complete();
    }

    public static void OneSoftBody(IPhysicsWorld physicsWorld)
    {
        var editor = physicsWorld.MakEditor();

        var square = editor.MakeSoftBody();
        var p1 = editor.AddMassPoint(square, new(400, 100));
        var p2 = editor.AddMassPoint(square, new(400, 200));
        var p3 = editor.AddMassPoint(square, new(600, 200));
        var p4 = editor.AddMassPoint(square, new(600, 100));
        editor.AddSpring(square, p1, p2);
        editor.AddSpring(square, p2, p3);
        editor.AddSpring(square, p3, p4);
        editor.AddSpring(square, p4, p1);
        editor.AddSpring(square, p1, p3);
        editor.AddSpring(square, p2, p4);

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(0, 100), new(10000, 100));

        editor.Complete();
    }

    public static void OneSoftBodyCollisions(IPhysicsWorld physicsWorld)
    {
        var editor = physicsWorld.MakEditor();

        var softBody = editor.MakeSoftBody();
        var p1 = editor.AddMassPoint(softBody, new(110, 800));
        var p2 = editor.AddMassPoint(softBody, new(180, 800));
        var p3 = editor.AddMassPoint(softBody, new(150, 900));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p3);

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 700), new(200, 600));

        editor.AddEdge(hardBody, new(400, 600), new(300, 600));

        editor.AddEdge(hardBody, new(150, 350), new(250, 450));

        editor.AddEdge(hardBody, new(400, 100), new(500, 200));
        editor.AddEdge(hardBody, new(100, 200), new(200, 100));

        editor.AddEdge(hardBody, new(500, 100), new(500, 1000));
        editor.AddEdge(hardBody, new(100, 100), new(100, 1000));

        editor.AddEdge(hardBody, new(100, 100), new(500, 100));

        editor.Complete();
    }

    public static void OneSoftBodyOneMassPointCollisions(IPhysicsWorld physicsWorld)
    {
        var editor = physicsWorld.MakEditor();

        var point = editor.MakeSoftBody();
        editor.AddMassPoint(point, new(500, 400));

        var square = editor.MakeSoftBody();
        var p1 = editor.AddMassPoint(square, new(400, 100));
        var p2 = editor.AddMassPoint(square, new(400, 200));
        var p3 = editor.AddMassPoint(square, new(600, 200));
        var p4 = editor.AddMassPoint(square, new(600, 100));
        editor.AddSpring(square, p1, p2);
        editor.AddSpring(square, p2, p3);
        editor.AddSpring(square, p3, p4);
        editor.AddSpring(square, p4, p1);
        editor.AddSpring(square, p1, p3);
        editor.AddSpring(square, p2, p4);

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(0, 100), new(10000, 100));

        editor.Complete();
    }

    public static void SoftBodyCollisions(IPhysicsWorld physicsWorld)
    {
        var editor = physicsWorld.MakEditor();

        var softBody = editor.MakeSoftBody();
        var p1 = editor.AddMassPoint(softBody, new(500, 200));
        var p2 = editor.AddMassPoint(softBody, new(500, 400));
        var p3 = editor.AddMassPoint(softBody, new(700, 400));
        var p4 = editor.AddMassPoint(softBody, new(700, 200));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p2, p3);
        editor.AddSpring(softBody, p3, p4);
        editor.AddSpring(softBody, p4, p1);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p4);

        softBody = editor.MakeSoftBody();
        p1 = editor.AddMassPoint(softBody, new(400, 1000));
        p2 = editor.AddMassPoint(softBody, new(400, 1200));
        p3 = editor.AddMassPoint(softBody, new(600, 1200));
        p4 = editor.AddMassPoint(softBody, new(600, 1000));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p2, p3);
        editor.AddSpring(softBody, p3, p4);
        editor.AddSpring(softBody, p4, p1);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p4);

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(0, 100), new(0, 1000));
        editor.AddEdge(hardBody, new(950, 100), new(950, 1000));
        editor.AddEdge(hardBody, new(0, 100), new(950, 100));

        editor.Complete();
    }

    public static void BigSoftBodySmallSoftBody(IPhysicsWorld physicsWorld)
    {
        var editor = physicsWorld.MakEditor();

        var softBody = editor.MakeSoftBody();
        var p1 = editor.AddMassPoint(softBody, new(200, 100));
        var p2 = editor.AddMassPoint(softBody, new(200, 200));
        var p3 = editor.AddMassPoint(softBody, new(800, 200));
        var p4 = editor.AddMassPoint(softBody, new(800, 100));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p2, p3);
        editor.AddSpring(softBody, p3, p4);
        editor.AddSpring(softBody, p4, p1);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p4);

        softBody = editor.MakeSoftBody();
        p1 = editor.AddMassPoint(softBody, new(400, 600));
        p2 = editor.AddMassPoint(softBody, new(400, 800));
        p3 = editor.AddMassPoint(softBody, new(600, 800));
        p4 = editor.AddMassPoint(softBody, new(600, 600));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p2, p3);
        editor.AddSpring(softBody, p3, p4);
        editor.AddSpring(softBody, p4, p1);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p4);

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(0, 100), new(0, 1000));
        editor.AddEdge(hardBody, new(950, 100), new(950, 1000));
        editor.AddEdge(hardBody, new(0, 100), new(950, 100));

        editor.Complete();
    }

    public static void SmallSoftBodyBigSoftBody(IPhysicsWorld physicsWorld)
    {
        var editor = physicsWorld.MakEditor();

        var softBody = editor.MakeSoftBody();
        var p1 = editor.AddMassPoint(softBody, new(400, 100));
        var p2 = editor.AddMassPoint(softBody, new(400, 200));
        var p3 = editor.AddMassPoint(softBody, new(600, 200));
        var p4 = editor.AddMassPoint(softBody, new(600, 100));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p2, p3);
        editor.AddSpring(softBody, p3, p4);
        editor.AddSpring(softBody, p4, p1);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p4);

        softBody = editor.MakeSoftBody();
        p1 = editor.AddMassPoint(softBody, new(200, 600));
        p2 = editor.AddMassPoint(softBody, new(200, 800));
        p3 = editor.AddMassPoint(softBody, new(800, 800));
        p4 = editor.AddMassPoint(softBody, new(800, 600));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p2, p3);
        editor.AddSpring(softBody, p3, p4);
        editor.AddSpring(softBody, p4, p1);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p4);

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(0, 100), new(0, 1000));
        editor.AddEdge(hardBody, new(950, 100), new(950, 1000));
        editor.AddEdge(hardBody, new(0, 100), new(950, 100));

        editor.Complete();
    }

    public static void SoftBodyVerticalCollisions(IPhysicsWorld physicsWorld)
    {
        var editor = physicsWorld.MakEditor();

        var softBody = editor.MakeSoftBody();
        var p1 = editor.AddMassPoint(softBody, new(400, 200));
        var p2 = editor.AddMassPoint(softBody, new(400, 400));
        var p3 = editor.AddMassPoint(softBody, new(600, 400));
        var p4 = editor.AddMassPoint(softBody, new(600, 200));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p2, p3);
        editor.AddSpring(softBody, p3, p4);
        editor.AddSpring(softBody, p4, p1);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p4);

        softBody = editor.MakeSoftBody();
        p1 = editor.AddMassPoint(softBody, new(400, 800));
        p2 = editor.AddMassPoint(softBody, new(400, 1000));
        p3 = editor.AddMassPoint(softBody, new(600, 1000));
        p4 = editor.AddMassPoint(softBody, new(600, 800));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p2, p3);
        editor.AddSpring(softBody, p3, p4);
        editor.AddSpring(softBody, p4, p1);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p4);

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(0, 100), new(0, 1000));
        editor.AddEdge(hardBody, new(950, 100), new(950, 1000));
        editor.AddEdge(hardBody, new(0, 100), new(950, 100));

        editor.Complete();
    }

    public static void SoftBodyCollisionsManyMassPoints(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.SpringStiffness = 10;

        var editor = physicsWorld.MakEditor();

        // one

        var softBody = editor.MakeSoftBody();

        var p10 = editor.AddMassPoint(softBody, new(300, 200));
        var p11 = editor.AddMassPoint(softBody, new(300, 250));
        var p12 = editor.AddMassPoint(softBody, new(300, 300));

        var p20 = editor.AddMassPoint(softBody, new(300, 350));
        var p21 = editor.AddMassPoint(softBody, new(350, 350));
        var p22 = editor.AddMassPoint(softBody, new(400, 350));
        var p23 = editor.AddMassPoint(softBody, new(450, 350));

        var p30 = editor.AddMassPoint(softBody, new(500, 350));
        var p31 = editor.AddMassPoint(softBody, new(500, 300));
        var p32 = editor.AddMassPoint(softBody, new(500, 250));
        var p33 = editor.AddMassPoint(softBody, new(500, 200));

        var p40 = editor.AddMassPoint(softBody, new(450, 200));
        var p41 = editor.AddMassPoint(softBody, new(400, 200));
        var p42 = editor.AddMassPoint(softBody, new(350, 200));

        editor.AddSpring(softBody, p10, p11);
        editor.AddSpring(softBody, p11, p12);

        editor.AddSpring(softBody, p12, p20);
        editor.AddSpring(softBody, p20, p21);
        editor.AddSpring(softBody, p21, p22);
        editor.AddSpring(softBody, p22, p23);

        editor.AddSpring(softBody, p23, p30);
        editor.AddSpring(softBody, p30, p31);
        editor.AddSpring(softBody, p31, p32);
        editor.AddSpring(softBody, p32, p33);

        editor.AddSpring(softBody, p33, p40);
        editor.AddSpring(softBody, p40, p41);
        editor.AddSpring(softBody, p41, p42);
        editor.AddSpring(softBody, p42, p10);

        editor.AddSpring(softBody, p10, p30);
        editor.AddSpring(softBody, p20, p33);

        editor.AddSpring(softBody, p21, p42);
        editor.AddSpring(softBody, p22, p41);
        editor.AddSpring(softBody, p23, p40);

        editor.AddSpring(softBody, p11, p32);
        editor.AddSpring(softBody, p12, p31);

        // two

        softBody = editor.MakeSoftBody();

        p10 = editor.AddMassPoint(softBody, new(300 + 120, 200 + 500));
        p11 = editor.AddMassPoint(softBody, new(300 + 120, 250 + 500));
        p12 = editor.AddMassPoint(softBody, new(300 + 120, 300 + 500));

        p20 = editor.AddMassPoint(softBody, new(300 + 120, 350 + 500));
        p21 = editor.AddMassPoint(softBody, new(350 + 120, 350 + 500));
        p22 = editor.AddMassPoint(softBody, new(400 + 120, 350 + 500));
        p23 = editor.AddMassPoint(softBody, new(450 + 120, 350 + 500));

        p30 = editor.AddMassPoint(softBody, new(500 + 120, 350 + 500));
        p31 = editor.AddMassPoint(softBody, new(500 + 120, 300 + 500));
        p32 = editor.AddMassPoint(softBody, new(500 + 120, 250 + 500));
        p33 = editor.AddMassPoint(softBody, new(500 + 120, 200 + 500));

        p40 = editor.AddMassPoint(softBody, new(450 + 120, 200 + 500));
        p41 = editor.AddMassPoint(softBody, new(400 + 120, 200 + 500));
        p42 = editor.AddMassPoint(softBody, new(350 + 120, 200 + 500));

        editor.AddSpring(softBody, p10, p11);
        editor.AddSpring(softBody, p11, p12);

        editor.AddSpring(softBody, p12, p20);
        editor.AddSpring(softBody, p20, p21);
        editor.AddSpring(softBody, p21, p22);
        editor.AddSpring(softBody, p22, p23);

        editor.AddSpring(softBody, p23, p30);
        editor.AddSpring(softBody, p30, p31);
        editor.AddSpring(softBody, p31, p32);
        editor.AddSpring(softBody, p32, p33);

        editor.AddSpring(softBody, p33, p40);
        editor.AddSpring(softBody, p40, p41);
        editor.AddSpring(softBody, p41, p42);
        editor.AddSpring(softBody, p42, p10);

        editor.AddSpring(softBody, p10, p30);
        editor.AddSpring(softBody, p20, p33);

        editor.AddSpring(softBody, p21, p42);
        editor.AddSpring(softBody, p22, p41);
        editor.AddSpring(softBody, p23, p40);

        editor.AddSpring(softBody, p11, p32);
        editor.AddSpring(softBody, p12, p31);

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(0, 100), new(0, 1000));
        editor.AddEdge(hardBody, new(1000, 100), new(1000, 1000));
        editor.AddEdge(hardBody, new(0, 100), new(1000, 100));

        editor.Complete();
    }

    public static void ManySoftBodyCollisions(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.SpringStiffness = 10;

        var editor = physicsWorld.MakEditor();

        var size = 40;
        for (int i = 0; i < 30; i++)
        {
            var x = 120;
            var y = 500 + 200 * i;
            var softBody = editor.MakeSoftBody();
            var p1 = editor.AddMassPoint(softBody, new(x + 10 * i, y));
            var p2 = editor.AddMassPoint(softBody, new(x + 10 * i, y + size));
            var p3 = editor.AddMassPoint(softBody, new(x + 10 * i + size, y + size));
            var p4 = editor.AddMassPoint(softBody, new(x + 10 * i + size, y));
            editor.AddSpring(softBody, p1, p2);
            editor.AddSpring(softBody, p2, p3);
            editor.AddSpring(softBody, p3, p4);
            editor.AddSpring(softBody, p4, p1);
            editor.AddSpring(softBody, p1, p3);
            editor.AddSpring(softBody, p2, p4);
        }

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 100), new(500, 100));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 100), new(100, 2000));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(500, 100), new(500, 2000));

        editor.Complete();
    }
}
