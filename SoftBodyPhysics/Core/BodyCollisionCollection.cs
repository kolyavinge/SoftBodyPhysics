using System.Collections.Generic;
using System.Linq;
using SoftBodyPhysics.Model;

namespace SoftBodyPhysics.Core;

internal interface IBodyCollisionCollection
{
    IEnumerable<int> GetCollidedSoftBodyIndexes(IBody body);

    IEnumerable<int> GetCollidedHardBodyIndexes(ISoftBody softBody);

    bool IsCollidedToAnySoftBody(IBody body);

    bool IsCollidedToAnyHardBody(ISoftBody softBody);

    void SetCollision(SoftBody body1, SoftBody body2);

    void SetCollision(SoftBody body1, HardBody body2);

    void ResetFor(SoftBody body);

    void UpdateForSoftBodies(SoftBody[] oldSoftBodies, SoftBody[] newSoftBodies);

    void UpdateForHardBodies(HardBody[] oldHardBodies, HardBody[] newHardBodies);
}

internal class BodyCollisionCollection : IBodyCollisionCollection
{
    private int _softBodiesCount;
    private int _hardBodiesCount;
    private bool[,] _softSoftMatrix;
    private bool[,] _softHardMatrix;

    public BodyCollisionCollection()
    {
        _softSoftMatrix = new bool[0, 0];
        _softHardMatrix = new bool[0, 0];
    }

    public IEnumerable<int> GetCollidedSoftBodyIndexes(IBody body)
    {
        if (body is SoftBody sb)
        {
            for (int i = 0; i < _softBodiesCount; i++)
            {
                if (_softSoftMatrix[sb.Index, i]) yield return i;
            }
        }
        else if (body is HardBody hb)
        {
            for (int i = 0; i < _softBodiesCount; i++)
            {
                if (_softHardMatrix[i, hb.Index]) yield return i;
            }
        }
    }

    public IEnumerable<int> GetCollidedHardBodyIndexes(ISoftBody softBody)
    {
        if (softBody is SoftBody sb)
        {
            for (int i = 0; i < _hardBodiesCount; i++)
            {
                if (_softHardMatrix[sb.Index, i]) yield return i;
            }
        }
    }

    public bool IsCollidedToAnySoftBody(IBody body)
    {
        if (body is SoftBody sb)
        {
            for (int i = 0; i < _softBodiesCount; i++)
            {
                if (_softSoftMatrix[sb.Index, i]) return true;
            }
        }
        else if (body is HardBody hb)
        {
            for (int i = 0; i < _softBodiesCount; i++)
            {
                if (_softHardMatrix[i, hb.Index]) return true;
            }
        }

        return false;
    }

    public bool IsCollidedToAnyHardBody(ISoftBody softBody)
    {
        if (softBody is SoftBody sb)
        {
            for (int i = 0; i < _hardBodiesCount; i++)
            {
                if (_softHardMatrix[sb.Index, i]) return true;
            }
        }

        return false;
    }

    public void SetCollision(SoftBody body1, SoftBody body2)
    {
        _softSoftMatrix[body1.Index, body2.Index] = true;
        _softSoftMatrix[body2.Index, body1.Index] = true;
    }

    public void SetCollision(SoftBody body1, HardBody body2)
    {
        _softHardMatrix[body1.Index, body2.Index] = true;
    }

    public void ResetFor(SoftBody body)
    {
        for (int i = 0; i < _softSoftMatrix.GetLength(0); i++)
        {
            _softSoftMatrix[body.Index, i] = false;
            _softSoftMatrix[i, body.Index] = false;
        }

        for (int i = 0; i < _softHardMatrix.GetLength(1); i++)
        {
            _softHardMatrix[body.Index, i] = false;
        }
    }

    public void UpdateForSoftBodies(SoftBody[] oldSoftBodies, SoftBody[] newSoftBodies)
    {
        var oldSoftSoftMatrix = _softSoftMatrix;
        _softBodiesCount = newSoftBodies.Length;
        _softSoftMatrix = new bool[_softBodiesCount, _softBodiesCount];
        var newSoftBodiesSet = newSoftBodies.ToHashSet();
        // update soft bodies matrix
        for (int i = 0; i < oldSoftSoftMatrix.GetLength(0) - 1; i++)
        {
            for (int j = i + 1; j < oldSoftSoftMatrix.GetLength(1); j++)
            {
                if (oldSoftSoftMatrix[i, j])
                {
                    var softBody1 = oldSoftBodies[i];
                    var softBody2 = oldSoftBodies[j];
                    if (newSoftBodiesSet.Contains(softBody1) && newSoftBodiesSet.Contains(softBody2))
                    {
                        _softSoftMatrix[softBody1.Index, softBody2.Index] = true;
                        _softSoftMatrix[softBody2.Index, softBody1.Index] = true;
                    }
                }
            }
        }
        // update hard bodies matrix
        var oldSoftHardMatrix = _softHardMatrix;
        _softHardMatrix = new bool[_softBodiesCount, _hardBodiesCount];
        for (int i = 0; i < oldSoftHardMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < oldSoftHardMatrix.GetLength(1); j++)
            {
                if (oldSoftHardMatrix[i, j])
                {
                    var softBody = oldSoftBodies[i];
                    if (newSoftBodiesSet.Contains(softBody))
                    {
                        _softHardMatrix[softBody.Index, j] = true;
                    }
                }
            }
        }
    }

    public void UpdateForHardBodies(HardBody[] oldHardBodies, HardBody[] newHardBodies)
    {
        var oldSoftHardMatrix = _softHardMatrix;
        _hardBodiesCount = newHardBodies.Length;
        _softHardMatrix = new bool[_softBodiesCount, _hardBodiesCount];
        var newHardBodiesSet = newHardBodies.ToHashSet();
        // update hard bodies matrix
        for (int i = 0; i < oldSoftHardMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < oldSoftHardMatrix.GetLength(1); j++)
            {
                if (oldSoftHardMatrix[i, j])
                {
                    var hardBody = oldHardBodies[j];
                    if (newHardBodiesSet.Contains(hardBody))
                    {
                        _softHardMatrix[i, hardBody.Index] = true;
                    }
                }
            }
        }
    }
}
