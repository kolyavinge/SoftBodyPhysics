using System;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface ISoftBodyCollisionChecker
{
    void CheckCollisions(SoftBody body1, SoftBody body2);
}

internal class SoftBodyCollisionChecker : ISoftBodyCollisionChecker
{
    private const float _delta = 1.0f;
    private readonly IMassPointSpringsCollisionChecker _collisionChecker;
    private readonly IBodyCollisionCollection _bodyCollisionCollection;

    public SoftBodyCollisionChecker(
        IMassPointSpringsCollisionChecker collisionChecker,
        IBodyCollisionCollection bodyCollisionCollection)
    {
        _collisionChecker = collisionChecker;
        _bodyCollisionCollection = bodyCollisionCollection;
    }

    public void CheckCollisions(SoftBody body1, SoftBody body2)
    {
        if (Math.Abs(body1.Borders.MiddleX - body2.Borders.MiddleX) > body1.Borders.HalfWidth + body2.Borders.HalfWidth + _delta) return;
        if (Math.Abs(body1.Borders.MiddleY - body2.Borders.MiddleY) > body1.Borders.HalfHeight + body2.Borders.HalfHeight + _delta) return;

        var hasCollisions = false;

        var massPoints = body1.EdgeMassPoints;
        for (var j = 0; j < massPoints.Length; j++)
        {
            var massPoint = massPoints[j];
            if (body2.Borders.MinX - _delta < massPoint.Position.x && massPoint.Position.x < body2.Borders.MaxX + _delta &&
                body2.Borders.MinY - _delta < massPoint.Position.y && massPoint.Position.y < body2.Borders.MaxY + _delta)
            {
                hasCollisions |= _collisionChecker.CheckMassPointAndSpringsCollision(massPoint, body2.Edges);
            }
        }

        massPoints = body2.EdgeMassPoints;
        for (var j = 0; j < massPoints.Length; j++)
        {
            var massPoint = massPoints[j];
            if (body1.Borders.MinX - _delta < massPoint.Position.x && massPoint.Position.x < body1.Borders.MaxX + _delta &&
                body1.Borders.MinY - _delta < massPoint.Position.y && massPoint.Position.y < body1.Borders.MaxY + _delta)
            {
                hasCollisions |= _collisionChecker.CheckMassPointAndSpringsCollision(massPoint, body1.Edges);
            }
        }

        if (hasCollisions)
        {
            _bodyCollisionCollection.SetCollision(body1, body2);
        }
    }
}
