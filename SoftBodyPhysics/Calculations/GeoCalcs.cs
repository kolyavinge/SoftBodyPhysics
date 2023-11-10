using System;

namespace SoftBodyPhysics.Calculations;

public static class GeoCalcs
{
    public static Vector RotatePoint(
        Vector point,
        Vector pivot,
        float angleRadian)
    {
        var result = RotatePoint(point.x, point.y, pivot.x, pivot.y, angleRadian);
        return new(result.x, result.y);
    }

    public static (float x, float y) RotatePoint(
        float pointX,
        float pointY,
        float pivotX,
        float pivotY,
        float angleRadian)
    {
        var cosAlpha = MathF.Cos(angleRadian);
        var sinAlpha = MathF.Sin(angleRadian);
        var x = cosAlpha * (pointX - pivotX) - sinAlpha * (pointY - pivotY) + pivotX;
        var y = sinAlpha * (pointX - pivotX) + cosAlpha * (pointY - pivotY) + pivotY;

        return (x, y);
    }
}
