using UnityEngine;

public interface IKnockable
{
    void KnockBack(Vector2 targetPos, float knockbackForce, int direction);
}
