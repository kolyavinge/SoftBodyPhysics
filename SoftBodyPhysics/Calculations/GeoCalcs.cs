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
        double cosAlpha = Math.Cos(angleRadian);
        double sinAlpha = Math.Sin(angleRadian);
        double x = cosAlpha * (pointX - pivotX) - sinAlpha * (pointY - pivotY) + pivotX;
        double y = sinAlpha * (pointX - pivotX) + cosAlpha * (pointY - pivotY) + pivotY;

        return ((float)x, (float)y);
    }
}
