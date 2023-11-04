namespace SoftBodyPhysics.Calculations;

internal static class PhysCalcs
{
    public static (float u1x, float u1y, float u2x, float u2y) GetNewVelocityAfterCollision(
        float mass1,
        float velocity1x,
        float velocity1y,
        float mass2,
        float velocity2x,
        float velocity2y)
    {
        float massSum = mass1 + mass2;
        float massDiff = mass1 - mass2;

        float u1x = (massDiff * velocity1x + 2.0f * mass2 * velocity2x) / massSum;
        float u1y = (massDiff * velocity1y + 2.0f * mass2 * velocity2y) / massSum;

        float u2x = (-massDiff * velocity2x + 2.0f * mass1 * velocity1x) / massSum;
        float u2y = (-massDiff * velocity2y + 2.0f * mass1 * velocity1y) / massSum;

        return (u1x, u1y, u2x, u2y);
    }
}
