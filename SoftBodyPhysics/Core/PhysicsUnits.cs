﻿namespace SoftBodyPhysics.Core;

public interface IPhysicsUnits
{
    float Mass { get; set; }

    float MassPointRadius { get; }

    float Time { get; set; }

    float SpringStiffness { get; set; }

    float SpringDamper { get; set; }

    float Sliding { get; set; }

    float GravityAcceleration { get; set; }
}

internal class PhysicsUnits : IPhysicsUnits
{
    public float Mass { get; set; }

    public float MassPointRadius { get; }

    public float Time { get; set; }

    public float SpringStiffness { get; set; }

    public float SpringDamper { get; set; }

    public float Sliding { get; set; }

    public float GravityAcceleration { get; set; }

    public PhysicsUnits()
    {
        Mass = Constants.Mass;
        MassPointRadius = Constants.MassPointRadius;
        Time = Constants.Time;
        SpringStiffness = Constants.SpringStiffness;
        SpringDamper = Constants.SpringDamper;
        Sliding = Constants.Sliding;
        GravityAcceleration = Constants.GravityAcceleration;
    }
}
