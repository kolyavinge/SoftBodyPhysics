using System;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface ISoftBodyCollisionChecker
{
    void CheckCollisions(SoftBody body);
}

internal class SoftBodyCollisionChecker : ISoftBodyCollisionChecker
{
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
        var delta = 2.0f;
        foreach (var body2 in _softBodiesCollection.SoftBodies)
        {
            if (body == body2) continue;

            if (Math.Abs(body.Borders.MiddleX - body2.Borders.MiddleX) > body.Borders.Width + body2.Borders.Width) continue;
            if (Math.Abs(body.Borders.MiddleY - body2.Borders.MiddleY) > body.Borders.Height + body2.Borders.Height) continue;

            foreach (var massPoint in body.MassPoints)
            {
                if (body2.Borders.MinX - delta < massPoint.Position.X && massPoint.Position.X < body2.Borders.MaxX + delta &&
                    body2.Borders.MinY - delta < massPoint.Position.Y && massPoint.Position.Y < body2.Borders.MaxY + delta)
                {
                    _collisionChecker.CheckMassPointAndSpringsCollision(massPoint, body2.SpringsToCheckCollisions);
                }
            }

            foreach (var massPoint in body2.MassPoints)
            {
                if (body.Borders.MinX - delta < massPoint.Position.X && massPoint.Position.X < body.Borders.MaxX + delta &&
                    body.Borders.MinY - delta < massPoint.Position.Y && massPoint.Position.Y < body.Borders.MaxY + delta)
                {
                    _collisionChecker.CheckMassPointAndSpringsCollision(massPoint, body.SpringsToCheckCollisions);
                }
            }
        }
    }
}
