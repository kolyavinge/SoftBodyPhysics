﻿using SoftBodyPhysics.Model;

namespace Examples;

public static class Example
{
    public static void OneMassPoint(IPhysicsWorld physicsWorld)
    {
        var editor = physicsWorld.MakEditor();
        var softBody = editor.MakeSoftBody();
        var p = editor.AddMassPoint(softBody, new(400, 250));
        p.Radius = 5;

        var hardBody = editor.AddHardBody();
        editor.AddEdge(hardBody, new(0, 100), new(10000, 100));

        editor.Complete();
    }

    public static void OneMassPointCollisions(IPhysicsWorld physicsWorld)
    {
        physicsWorld.Units.Friction = 0.1;

        var editor = physicsWorld.MakEditor();

        var softBody = editor.MakeSoftBody();
        var p = editor.AddMassPoint(softBody, new(150, 1000));
        p.Radius = 5;

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

        var softBody = editor.MakeSoftBody();
        var p1 = editor.AddMassPoint(softBody, new(300, 300));
        var p2 = editor.AddMassPoint(softBody, new(400, 500));
        var p3 = editor.AddMassPoint(softBody, new(500, 400));
        editor.AddSpring(softBody, p1, p2);
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p3);

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

    public static void OneSoftBodyStay(IPhysicsWorld physicsWorld)
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
        var s = editor.AddSpring(softBody, p1, p2);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p2, p3);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p3, p4);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p4, p1);
        s.IsEdge = true;
        editor.AddSpring(softBody, p1, p3);
        editor.AddSpring(softBody, p2, p4);

        softBody = editor.MakeSoftBody();
        p1 = editor.AddMassPoint(softBody, new(400, 1000));
        p2 = editor.AddMassPoint(softBody, new(400, 1200));
        p3 = editor.AddMassPoint(softBody, new(600, 1200));
        p4 = editor.AddMassPoint(softBody, new(600, 1000));
        s = editor.AddSpring(softBody, p1, p2);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p2, p3);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p3, p4);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p4, p1);
        s.IsEdge = true;
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

        var s = editor.AddSpring(softBody, p10, p11);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p11, p12);
        s.IsEdge = true;

        s = editor.AddSpring(softBody, p12, p20);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p20, p21);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p21, p22);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p22, p23);
        s.IsEdge = true;

        s = editor.AddSpring(softBody, p23, p30);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p30, p31);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p31, p32);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p32, p33);
        s.IsEdge = true;

        s = editor.AddSpring(softBody, p33, p40);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p40, p41);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p41, p42);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p42, p10);
        s.IsEdge = true;

        editor.AddSpring(softBody, p10, p30);
        editor.AddSpring(softBody, p20, p33);

        editor.AddSpring(softBody, p21, p42);
        editor.AddSpring(softBody, p22, p41);
        editor.AddSpring(softBody, p23, p40);

        editor.AddSpring(softBody, p11, p32);
        editor.AddSpring(softBody, p12, p31);

        // two

        softBody = editor.MakeSoftBody();

        p10 = editor.AddMassPoint(softBody, new(300 + 120, 200));
        p11 = editor.AddMassPoint(softBody, new(300 + 120, 250));
        p12 = editor.AddMassPoint(softBody, new(300 + 120, 300));

        p20 = editor.AddMassPoint(softBody, new(300 + 120, 350));
        p21 = editor.AddMassPoint(softBody, new(350 + 120, 350));
        p22 = editor.AddMassPoint(softBody, new(400 + 120, 350));
        p23 = editor.AddMassPoint(softBody, new(450 + 120, 350));

        p30 = editor.AddMassPoint(softBody, new(500 + 120, 350));
        p31 = editor.AddMassPoint(softBody, new(500 + 120, 300));
        p32 = editor.AddMassPoint(softBody, new(500 + 120, 250));
        p33 = editor.AddMassPoint(softBody, new(500 + 120, 200));

        p40 = editor.AddMassPoint(softBody, new(450 + 120, 200));
        p41 = editor.AddMassPoint(softBody, new(400 + 120, 200));
        p42 = editor.AddMassPoint(softBody, new(350 + 120, 200));

        s = editor.AddSpring(softBody, p10, p11);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p11, p12);
        s.IsEdge = true;

        s = editor.AddSpring(softBody, p12, p20);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p20, p21);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p21, p22);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p22, p23);
        s.IsEdge = true;

        s = editor.AddSpring(softBody, p23, p30);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p30, p31);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p31, p32);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p32, p33);
        s.IsEdge = true;

        s = editor.AddSpring(softBody, p33, p40);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p40, p41);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p41, p42);
        s.IsEdge = true;
        s = editor.AddSpring(softBody, p42, p10);
        s.IsEdge = true;

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
}
