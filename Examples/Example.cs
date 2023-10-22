using SoftBodyPhysics.Core;
using SoftBodyPhysics.Model;

namespace Examples;

public static class Example
{
    public static void OnePointCollisions(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.Sliding = 0.1f;

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

    public static void OneBody(IPhysicsWorld physicsWorld)
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

    public static void OneBodyCollisions(IPhysicsWorld physicsWorld)
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

    public static void OneBodyOnePointCollisions(IPhysicsWorld physicsWorld)
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

    public static void TwoBodiesCollisions(IPhysicsWorld physicsWorld)
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

    public static void TwoBodiesVerticalCollisions(IPhysicsWorld physicsWorld)
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

    public static void TwoBodiesManyPointsCollisions(IPhysicsWorld physicsWorld)
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

    public static void ManyBodiesCollisions(IPhysicsWorld physicsWorld)
    {
        //physicsWorld.Units.Time = 0.2f;
        physicsWorld.Units.SpringStiffness = 100;

        var editor = physicsWorld.MakEditor();

        var size = 40;
        for (int i = 0; i < 40; i++)
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
        editor.AddEdge(hardBody, new(100, 100), new(800, 100));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 100), new(100, 20000));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(800, 100), new(800, 20000));

        editor.Complete();
    }

    public static void VeryFast(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.SpringStiffness = 10;

        var editor = physicsWorld.MakEditor();

        var softBody = editor.MakeSoftBody();
        var p1 = editor.AddMassPoint(softBody, new(120, 950));
        var p2 = editor.AddMassPoint(softBody, new(120, 970));
        var p3 = editor.AddMassPoint(softBody, new(140, 970));
        var p4 = editor.AddMassPoint(softBody, new(140, 950));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p2, p3);
        editor.AddSpring(softBody, p3, p4);
        editor.AddSpring(softBody, p4, p1);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p4);

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 100), new(800, 100));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 100), new(100, 1000));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(800, 100), new(800, 1000));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 1000), new(800, 1000));

        editor.Complete();

        foreach (var m in softBody.MassPoints)
        {
            m.Velocity = new(10000, -10000);
        }
    }

    public static void LongPipe(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.Time = 0.05f;
        physicsWorld.Units.SpringStiffness = 50;
        physicsWorld.Units.SpringDamper = 10;

        var editor = physicsWorld.MakEditor();

        var softBody = editor.MakeSoftBody();
        var size = 20;
        var points = new IMassPoint[10, 4];
        for (int i = 0; i < points.GetLength(0); i++)
        {
            for (int j = 0; j < points.GetLength(1); j++)
            {
                points[i, j] = editor.AddMassPoint(softBody, new(400 + j * size, 600 + i * size));
            }
        }
        for (int i = 0; i < points.GetLength(0); i++)
        {
            for (int j = 1; j < points.GetLength(1); j++)
            {
                editor.AddSpring(softBody, points[i, j], points[i, j - 1]);
            }
        }
        for (int i = 1; i < points.GetLength(0); i++)
        {
            for (int j = 0; j < points.GetLength(1); j++)
            {
                editor.AddSpring(softBody, points[i, j], points[i - 1, j]);
            }
        }

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 100), new(800, 100));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 100), new(100, 1000));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(800, 100), new(800, 1000));

        editor.Complete();
    }

    public static void BigCube(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.SpringStiffness = 50;

        var editor = physicsWorld.MakEditor();

        var softBody = editor.MakeSoftBody();
        var size = 20;
        var points = new IMassPoint[35, 27];
        for (int i = 0; i < points.GetLength(0); i++)
        {
            for (int j = 0; j < points.GetLength(1); j++)
            {
                points[i, j] = editor.AddMassPoint(softBody, new(200 + j * size, 100 + i * size));
            }
        }
        for (int i = 0; i < points.GetLength(0); i++)
        {
            for (int j = 1; j < points.GetLength(1); j++)
            {
                editor.AddSpring(softBody, points[i, j], points[i, j - 1]);
            }
        }
        for (int i = 1; i < points.GetLength(0); i++)
        {
            for (int j = 0; j < points.GetLength(1); j++)
            {
                editor.AddSpring(softBody, points[i, j], points[i - 1, j]);
            }
        }

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 100), new(800, 100));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(100, 100), new(100, 1000));

        hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(800, 100), new(800, 1000));

        editor.Complete();
    }
}
