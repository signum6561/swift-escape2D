using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockable
{
    void KnockBack(Vector2 targetPos, float knockbackForce, int direction);
}
