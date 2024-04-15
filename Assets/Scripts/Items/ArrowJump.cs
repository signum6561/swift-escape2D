using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowJump : Item
{
    protected override void OnHitPlayerEnter(Collider2D col)
    {
        base.OnHitPlayerEnter(col);
        IPushable pushable = col.GetComponent<IPushable>();
        pushable?.AddForce(new Vector2(0, 30f));
    }
}
