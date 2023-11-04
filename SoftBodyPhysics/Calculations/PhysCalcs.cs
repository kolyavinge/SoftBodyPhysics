namespace SoftBodyPhysics.Calculations;

internal static class PhysCalcs
{
    public static (Vector u1, Vector u2) GetNewVelocityAfterCollision(
        float m1,
        Vector v1,
        float m2,
        Vector v2)
    {
        float massSum = m1 + m2;
        float massDiff = m1 - m2;

        float u1x = (massDiff * v1.x + 2.0f * m2 * v2.x) / massSum;
        float u1y = (massDiff * v1.y + 2.0f * m2 * v2.y) / massSum;

        float u2x = (-massDiff * v2.x + 2.0f * m1 * v1.x) / massSum;
        float u2y = (-massDiff * v2.y + 2.0f * m1 * v1.y) / massSum;

        return (new(u1x, u1y), new(u2x, u2y));
    }
}
