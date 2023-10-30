using SoftBodyPhysics.Geo;

namespace SoftBodyPhysics.Ancillary;

internal static class PhysCalcs
{
    public static (Vector u1, Vector u2) GetNewVelocityAfterCollision(
        float m1,
        Vector v1,
        float m2,
        Vector v2)
    {
        float u1x = ((m1 - m2) * v1.x + 2.0f * m2 * v2.x) / (m1 + m2);
        float u1y = ((m1 - m2) * v1.y + 2.0f * m2 * v2.y) / (m1 + m2);

        float u2x = ((m2 - m1) * v2.x + 2.0f * m1 * v1.x) / (m1 + m2);
        float u2y = ((m2 - m1) * v2.y + 2.0f * m1 * v1.y) / (m1 + m2);

        return (new(u1x, u1y), new(u2x, u2y));
    }
}
