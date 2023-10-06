namespace SoftBodyPhysics.Model;

public interface IPhysicsUnits
{
    double Mass { get; set; }

    double MassPointRadius { get; }

    double Time { get; set; }

    double SpringStiffness { get; set; }

    double SpringDamper { get; set; }

    double Friction { get; set; }

    double GravityAcceleration { get; set; }
}

internal class PhysicsUnits : IPhysicsUnits
{
    public double Mass { get; set; }

    public double MassPointRadius { get; }

    public double Time { get; set; }

    public double SpringStiffness { get; set; }

    public double SpringDamper { get; set; }

    public double Friction { get; set; }

    public double GravityAcceleration { get; set; }

    public PhysicsUnits()
    {
        Mass = Constants.Mass;
        MassPointRadius = Constants.MassPointRadius;
        Time = Constants.Time;
        SpringStiffness = Constants.SpringStiffness;
        SpringDamper = Constants.SpringDamper;
        Friction = Constants.Friction;
        GravityAcceleration = Constants.GravityAcceleration;
    }
}
