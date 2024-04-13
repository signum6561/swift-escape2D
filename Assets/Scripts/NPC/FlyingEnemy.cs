using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    protected override void DeadEnter()
    {
        Rb.bodyType = RigidbodyType2D.Dynamic;
        Rb.gravityScale = 10;
        base.DeadEnter();
    }
}
