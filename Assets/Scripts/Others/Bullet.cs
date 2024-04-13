using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float timeDestroy = 2f;
    private enum BulletType
    {
        BulletPlant = ObjectType.BulletPlant,
        BulletTrunk = ObjectType.BulletTrunk
    }
    [SerializeField] private BulletType bulletType;
    protected void OnEnable()
    {
        Invoke(nameof(Disappear), timeDestroy);
    }
    protected virtual void FixedUpdate()
    {
        transform.Translate(moveSpeed * Time.fixedDeltaTime * Vector2.right);
    }
    protected void OnDisable()
    {
        CancelInvoke();
    }
    protected virtual void Disappear()
    {
        PoolManager.Instance.CoolObject(gameObject, (ObjectType)bulletType);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == (int)LayerIndex.Player)
        {
            IDamageable damageable = col.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
            Disappear();
        }
        else if (col.gameObject.layer == (int)LayerIndex.Ground)
        {
            Disappear();
        }
    }
}
