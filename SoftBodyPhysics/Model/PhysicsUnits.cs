namespace SoftBodyPhysics.Model;

public interface IPhysicsUnits
{
    double Mass { get; set; }

    double MassPointRadius { get; set; }

    double TimeDelta { get; }

    double SpringStiffness { get; set; }

    double SpringDamper { get; set; }

    double Friction { get; set; }

    double GravityAcceleration { get; set; }
}

internal class PhysicsUnits : IPhysicsUnits
{
    public double Mass { get; set; }

    public double MassPointRadius { get; set; }

    public double TimeDelta { get; }

    public double SpringStiffness { get; set; }

    public double SpringDamper { get; set; }

    public double Friction { get; set; }

    public double GravityAcceleration { get; set; }

    public PhysicsUnits()
    {
        Mass = Constants.Mass;
        MassPointRadius = Constants.MassPointRadius;
        TimeDelta = Constants.TimeDelta;
        SpringStiffness = Constants.SpringStiffness;
        SpringDamper = Constants.SpringDamper;
        Friction = Constants.Friction;
        GravityAcceleration = Constants.GravityAcceleration;
    }
}
