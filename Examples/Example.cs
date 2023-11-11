using SoftBodyPhysics.Core;
using SoftBodyPhysics.Model;

namespace Examples;

public static class Example
{
    public static void OnePointCollisions(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.Sliding = 0.1f;

        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        softBodyEditor.AddMassPoint(new(150, 1000));

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 700), new(200, 600));

        hardBodyEditor.AddEdge(hardBody, new(400, 600), new(300, 600));

        hardBodyEditor.AddEdge(hardBody, new(150, 350), new(250, 450));

        hardBodyEditor.AddEdge(hardBody, new(400, 100), new(500, 200));
        hardBodyEditor.AddEdge(hardBody, new(100, 200), new(200, 100));

        hardBodyEditor.AddEdge(hardBody, new(500, 100), new(500, 1000));
        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(100, 1000));

        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(500, 100));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();
    }

    public static void OneBody(IPhysicsWorld physicsWorld)
    {
        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        var p1 = softBodyEditor.AddMassPoint(new(400, 100));
        var p2 = softBodyEditor.AddMassPoint(new(400, 200));
        var p3 = softBodyEditor.AddMassPoint(new(600, 200));
        var p4 = softBodyEditor.AddMassPoint(new(600, 100));
        softBodyEditor.AddSpring(p1, p2);
        softBodyEditor.AddSpring(p2, p3);
        softBodyEditor.AddSpring(p3, p4);
        softBodyEditor.AddSpring(p4, p1);
        softBodyEditor.AddSpring(p1, p3);
        softBodyEditor.AddSpring(p2, p4);

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(0, 100), new(10000, 100));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();
    }

    public static void OneBodyCollisions(IPhysicsWorld physicsWorld)
    {
        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        var p1 = softBodyEditor.AddMassPoint(new(110, 800));
        var p2 = softBodyEditor.AddMassPoint(new(180, 800));
        var p3 = softBodyEditor.AddMassPoint(new(150, 900));
        softBodyEditor.AddSpring(p1, p2);
        softBodyEditor.AddSpring(p1, p3);
        softBodyEditor.AddSpring(p2, p3);

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 700), new(200, 600));

        hardBodyEditor.AddEdge(hardBody, new(400, 600), new(300, 600));

        hardBodyEditor.AddEdge(hardBody, new(150, 350), new(250, 450));

        hardBodyEditor.AddEdge(hardBody, new(400, 100), new(500, 200));
        hardBodyEditor.AddEdge(hardBody, new(100, 200), new(200, 100));

        hardBodyEditor.AddEdge(hardBody, new(500, 100), new(500, 1000));
        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(100, 1000));

        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(500, 100));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();
    }

    public static void OneBodyOnePointCollisions(IPhysicsWorld physicsWorld)
    {
        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        softBodyEditor.AddMassPoint(new(500, 400));

        var p1 = softBodyEditor.AddMassPoint(new(400, 100));
        var p2 = softBodyEditor.AddMassPoint(new(400, 200));
        var p3 = softBodyEditor.AddMassPoint(new(600, 200));
        var p4 = softBodyEditor.AddMassPoint(new(600, 100));
        softBodyEditor.AddSpring(p1, p2);
        softBodyEditor.AddSpring(p2, p3);
        softBodyEditor.AddSpring(p3, p4);
        softBodyEditor.AddSpring(p4, p1);
        softBodyEditor.AddSpring(p1, p3);
        softBodyEditor.AddSpring(p2, p4);

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(0, 100), new(10000, 100));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();
    }

    public static void TwoBodiesCollisions(IPhysicsWorld physicsWorld)
    {
        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        var p1 = softBodyEditor.AddMassPoint(new(500, 200));
        var p2 = softBodyEditor.AddMassPoint(new(500, 400));
        var p3 = softBodyEditor.AddMassPoint(new(700, 400));
        var p4 = softBodyEditor.AddMassPoint(new(700, 200));
        softBodyEditor.AddSpring(p1, p2);
        softBodyEditor.AddSpring(p2, p3);
        softBodyEditor.AddSpring(p3, p4);
        softBodyEditor.AddSpring(p4, p1);
        softBodyEditor.AddSpring(p1, p3);
        softBodyEditor.AddSpring(p2, p4);

        p1 = softBodyEditor.AddMassPoint(new(400, 1000));
        p2 = softBodyEditor.AddMassPoint(new(400, 1200));
        p3 = softBodyEditor.AddMassPoint(new(600, 1200));
        p4 = softBodyEditor.AddMassPoint(new(600, 1000));
        softBodyEditor.AddSpring(p1, p2);
        softBodyEditor.AddSpring(p2, p3);
        softBodyEditor.AddSpring(p3, p4);
        softBodyEditor.AddSpring(p4, p1);
        softBodyEditor.AddSpring(p1, p3);
        softBodyEditor.AddSpring(p2, p4);

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(0, 100), new(0, 1000));
        hardBodyEditor.AddEdge(hardBody, new(950, 100), new(950, 1000));
        hardBodyEditor.AddEdge(hardBody, new(0, 100), new(950, 100));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();
    }

    public static void TwoBodiesVerticalCollisions(IPhysicsWorld physicsWorld)
    {
        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        var p1 = softBodyEditor.AddMassPoint(new(400, 200));
        var p2 = softBodyEditor.AddMassPoint(new(400, 400));
        var p3 = softBodyEditor.AddMassPoint(new(600, 400));
        var p4 = softBodyEditor.AddMassPoint(new(600, 200));
        softBodyEditor.AddSpring(p1, p2);
        softBodyEditor.AddSpring(p2, p3);
        softBodyEditor.AddSpring(p3, p4);
        softBodyEditor.AddSpring(p4, p1);
        softBodyEditor.AddSpring(p1, p3);
        softBodyEditor.AddSpring(p2, p4);

        p1 = softBodyEditor.AddMassPoint(new(400, 800));
        p2 = softBodyEditor.AddMassPoint(new(400, 1000));
        p3 = softBodyEditor.AddMassPoint(new(600, 1000));
        p4 = softBodyEditor.AddMassPoint(new(600, 800));
        softBodyEditor.AddSpring(p1, p2);
        softBodyEditor.AddSpring(p2, p3);
        softBodyEditor.AddSpring(p3, p4);
        softBodyEditor.AddSpring(p4, p1);
        softBodyEditor.AddSpring(p1, p3);
        softBodyEditor.AddSpring(p2, p4);

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(0, 100), new(0, 1000));
        hardBodyEditor.AddEdge(hardBody, new(950, 100), new(950, 1000));
        hardBodyEditor.AddEdge(hardBody, new(0, 100), new(950, 100));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();
    }

    public static void TwoBodiesManyPointsCollisions(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.SpringStiffness = 10;

        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        // one

        var p10 = softBodyEditor.AddMassPoint(new(300, 200));
        var p11 = softBodyEditor.AddMassPoint(new(300, 250));
        var p12 = softBodyEditor.AddMassPoint(new(300, 300));

        var p20 = softBodyEditor.AddMassPoint(new(300, 350));
        var p21 = softBodyEditor.AddMassPoint(new(350, 350));
        var p22 = softBodyEditor.AddMassPoint(new(400, 350));
        var p23 = softBodyEditor.AddMassPoint(new(450, 350));

        var p30 = softBodyEditor.AddMassPoint(new(500, 350));
        var p31 = softBodyEditor.AddMassPoint(new(500, 300));
        var p32 = softBodyEditor.AddMassPoint(new(500, 250));
        var p33 = softBodyEditor.AddMassPoint(new(500, 200));

        var p40 = softBodyEditor.AddMassPoint(new(450, 200));
        var p41 = softBodyEditor.AddMassPoint(new(400, 200));
        var p42 = softBodyEditor.AddMassPoint(new(350, 200));

        softBodyEditor.AddSpring(p10, p11);
        softBodyEditor.AddSpring(p11, p12);

        softBodyEditor.AddSpring(p12, p20);
        softBodyEditor.AddSpring(p20, p21);
        softBodyEditor.AddSpring(p21, p22);
        softBodyEditor.AddSpring(p22, p23);

        softBodyEditor.AddSpring(p23, p30);
        softBodyEditor.AddSpring(p30, p31);
        softBodyEditor.AddSpring(p31, p32);
        softBodyEditor.AddSpring(p32, p33);

        softBodyEditor.AddSpring(p33, p40);
        softBodyEditor.AddSpring(p40, p41);
        softBodyEditor.AddSpring(p41, p42);
        softBodyEditor.AddSpring(p42, p10);

        softBodyEditor.AddSpring(p10, p30);
        softBodyEditor.AddSpring(p20, p33);

        softBodyEditor.AddSpring(p21, p42);
        softBodyEditor.AddSpring(p22, p41);
        softBodyEditor.AddSpring(p23, p40);

        softBodyEditor.AddSpring(p11, p32);
        softBodyEditor.AddSpring(p12, p31);

        // two

        p10 = softBodyEditor.AddMassPoint(new(300 + 120, 200 + 500));
        p11 = softBodyEditor.AddMassPoint(new(300 + 120, 250 + 500));
        p12 = softBodyEditor.AddMassPoint(new(300 + 120, 300 + 500));

        p20 = softBodyEditor.AddMassPoint(new(300 + 120, 350 + 500));
        p21 = softBodyEditor.AddMassPoint(new(350 + 120, 350 + 500));
        p22 = softBodyEditor.AddMassPoint(new(400 + 120, 350 + 500));
        p23 = softBodyEditor.AddMassPoint(new(450 + 120, 350 + 500));

        p30 = softBodyEditor.AddMassPoint(new(500 + 120, 350 + 500));
        p31 = softBodyEditor.AddMassPoint(new(500 + 120, 300 + 500));
        p32 = softBodyEditor.AddMassPoint(new(500 + 120, 250 + 500));
        p33 = softBodyEditor.AddMassPoint(new(500 + 120, 200 + 500));

        p40 = softBodyEditor.AddMassPoint(new(450 + 120, 200 + 500));
        p41 = softBodyEditor.AddMassPoint(new(400 + 120, 200 + 500));
        p42 = softBodyEditor.AddMassPoint(new(350 + 120, 200 + 500));

        softBodyEditor.AddSpring(p10, p11);
        softBodyEditor.AddSpring(p11, p12);

        softBodyEditor.AddSpring(p12, p20);
        softBodyEditor.AddSpring(p20, p21);
        softBodyEditor.AddSpring(p21, p22);
        softBodyEditor.AddSpring(p22, p23);

        softBodyEditor.AddSpring(p23, p30);
        softBodyEditor.AddSpring(p30, p31);
        softBodyEditor.AddSpring(p31, p32);
        softBodyEditor.AddSpring(p32, p33);

        softBodyEditor.AddSpring(p33, p40);
        softBodyEditor.AddSpring(p40, p41);
        softBodyEditor.AddSpring(p41, p42);
        softBodyEditor.AddSpring(p42, p10);

        softBodyEditor.AddSpring(p10, p30);
        softBodyEditor.AddSpring(p20, p33);

        softBodyEditor.AddSpring(p21, p42);
        softBodyEditor.AddSpring(p22, p41);
        softBodyEditor.AddSpring(p23, p40);

        softBodyEditor.AddSpring(p11, p32);
        softBodyEditor.AddSpring(p12, p31);

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(0, 100), new(0, 1000));
        hardBodyEditor.AddEdge(hardBody, new(1000, 100), new(1000, 1000));
        hardBodyEditor.AddEdge(hardBody, new(0, 100), new(1000, 100));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();
    }

    public static void ManyBodiesCollisions(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.Sliding = 0.9f;
        physicsWorld.Units.SpringStiffness = 200;
        physicsWorld.Units.SpringDamper = 1;

        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        var size = 40;
        for (int i = 0; i < 40; i++)
        {
            var x = 120;
            var y = 500 + 200 * i;
            var p1 = softBodyEditor.AddMassPoint(new(x + 10 * i, y));
            var p2 = softBodyEditor.AddMassPoint(new(x + 10 * i, y + size));
            var p3 = softBodyEditor.AddMassPoint(new(x + 10 * i + size, y + size));
            var p4 = softBodyEditor.AddMassPoint(new(x + 10 * i + size, y));
            softBodyEditor.AddSpring(p1, p2);
            softBodyEditor.AddSpring(p2, p3);
            softBodyEditor.AddSpring(p3, p4);
            softBodyEditor.AddSpring(p4, p1);
            softBodyEditor.AddSpring(p1, p3);
            softBodyEditor.AddSpring(p2, p4);
        }

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(800, 100));

        hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(100, 20000));

        hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(800, 100), new(800, 20000));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();
    }

    public static void VeryFast(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.SpringStiffness = 10;

        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        var p1 = softBodyEditor.AddMassPoint(new(120, 950));
        var p2 = softBodyEditor.AddMassPoint(new(120, 970));
        var p3 = softBodyEditor.AddMassPoint(new(140, 970));
        var p4 = softBodyEditor.AddMassPoint(new(140, 950));
        softBodyEditor.AddSpring(p1, p2);
        softBodyEditor.AddSpring(p2, p3);
        softBodyEditor.AddSpring(p3, p4);
        softBodyEditor.AddSpring(p4, p1);
        softBodyEditor.AddSpring(p1, p3);
        softBodyEditor.AddSpring(p2, p4);

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(800, 100));

        hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(100, 1000));

        hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(800, 100), new(800, 1000));

        hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 1000), new(800, 1000));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();

        foreach (var sb in physicsWorld.SoftBodies)
        {
            foreach (var m in sb.MassPoints)
            {
                m.Velocity = new(10000, -10000);
            }
        }
    }

    public static void LongPipe(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.Time = 0.05f;
        physicsWorld.Units.SpringStiffness = 50;
        physicsWorld.Units.SpringDamper = 10;

        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        var size = 20;
        var points = new IMassPoint[10, 4];
        for (int i = 0; i < points.GetLength(0); i++)
        {
            for (int j = 0; j < points.GetLength(1); j++)
            {
                points[i, j] = softBodyEditor.AddMassPoint(new(400 + j * size, 600 + i * size));
            }
        }
        for (int i = 0; i < points.GetLength(0); i++)
        {
            for (int j = 1; j < points.GetLength(1); j++)
            {
                softBodyEditor.AddSpring(points[i, j], points[i, j - 1]);
            }
        }
        for (int i = 1; i < points.GetLength(0); i++)
        {
            for (int j = 0; j < points.GetLength(1); j++)
            {
                softBodyEditor.AddSpring(points[i, j], points[i - 1, j]);
            }
        }

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(800, 100));

        hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(100, 1000));

        hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(800, 100), new(800, 1000));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();
    }

    public static void BigCube(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.SpringStiffness = 50;

        var softBodyEditor = physicsWorld.MakeSoftBodyEditor();
        var hardBodyEditor = physicsWorld.MakeHardBodyEditor();

        var size = 20;
        var points = new IMassPoint[35, 27];
        for (int i = 0; i < points.GetLength(0); i++)
        {
            for (int j = 0; j < points.GetLength(1); j++)
            {
                points[i, j] = softBodyEditor.AddMassPoint(new(200 + j * size, 100 + i * size));
            }
        }
        for (int i = 0; i < points.GetLength(0); i++)
        {
            for (int j = 1; j < points.GetLength(1); j++)
            {
                softBodyEditor.AddSpring(points[i, j], points[i, j - 1]);
            }
        }
        for (int i = 1; i < points.GetLength(0); i++)
        {
            for (int j = 0; j < points.GetLength(1); j++)
            {
                softBodyEditor.AddSpring(points[i, j], points[i - 1, j]);
            }
        }

        var hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(800, 100));

        hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(100, 100), new(100, 1000));

        hardBody = hardBodyEditor.AddHardBody();
        hardBodyEditor.AddEdge(hardBody, new(800, 100), new(800, 1000));

        softBodyEditor.Complete();
        hardBodyEditor.Complete();
    }
}
