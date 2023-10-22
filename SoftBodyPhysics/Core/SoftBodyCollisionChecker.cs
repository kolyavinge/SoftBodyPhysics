using System;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface ISoftBodyCollisionChecker
{
    void CheckCollisions(SoftBody body);
}

internal class SoftBodyCollisionChecker : ISoftBodyCollisionChecker
{
    private const float _delta = 2.0f;
    private readonly ISoftBodiesCollection _softBodiesCollection;
    private readonly IMassPointSpringsCollisionChecker _collisionChecker;

    public SoftBodyCollisionChecker(
        ISoftBodiesCollection softBodiesCollection,
        IMassPointSpringsCollisionChecker collisionChecker)
    {
        _softBodiesCollection = softBodiesCollection;
        _collisionChecker = collisionChecker;
    }

    public void CheckCollisions(SoftBody body)
    {
        foreach (var body2 in _softBodiesCollection.SoftBodies)
        {
            if (body == body2) continue;

            if (Math.Abs(body.Borders.MiddleX - body2.Borders.MiddleX) > body.Borders.Width + body2.Borders.Width) continue;
            if (Math.Abs(body.Borders.MiddleY - body2.Borders.MiddleY) > body.Borders.Height + body2.Borders.Height) continue;

            foreach (var massPoint in body.MassPoints)
            {
                if (body2.Borders.MinX - _delta < massPoint.Position.x && massPoint.Position.x < body2.Borders.MaxX + _delta &&
                    body2.Borders.MinY - _delta < massPoint.Position.y && massPoint.Position.y < body2.Borders.MaxY + _delta)
                {
                    _collisionChecker.CheckMassPointAndSpringsCollision(massPoint, body2.SpringsToCheckCollisions);
                }
            }

            foreach (var massPoint in body2.MassPoints)
            {
                if (body.Borders.MinX - _delta < massPoint.Position.x && massPoint.Position.x < body.Borders.MaxX + _delta &&
                    body.Borders.MinY - _delta < massPoint.Position.y && massPoint.Position.y < body.Borders.MaxY + _delta)
                {
                    _collisionChecker.CheckMassPointAndSpringsCollision(massPoint, body.SpringsToCheckCollisions);
                }
            }
        }
    }
}
