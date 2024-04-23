using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item, IPushable
{
    public Rigidbody2D Rb { get; private set; }
    public void AddForce(Vector2 pushForce)
    {
        if (Rb == null)
        {
            Rb = GetComponent<Rigidbody2D>();
        }
        Rb.bodyType = RigidbodyType2D.Dynamic;
        Rb.velocity = pushForce;
    }
    public override void TriggerAnimationFinished()
    {
        isAnimationFinished = true;
        PoolManager.Instance.CoolObject(gameObject, ObjectType.Heart);
    }
    protected override void OnHitPlayerEnter(Collider2D col)
    {
        base.OnHitPlayerEnter(col);
        AudioManager.Instance.PlaySFX("powerUp", SoundType.Other);
        IHealable healable = col.GetComponent<IHealable>();
        healable?.Heal();
    }
}
