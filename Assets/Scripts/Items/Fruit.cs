using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Item, IPushable
{
    public static event Action<ItemType> OnCollected;
    [SerializeField] private ItemType fruitType;
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
        OnCollected?.Invoke(fruitType);
        PoolManager.Instance.CoolObject(gameObject, (ObjectType)fruitType);
    }
}
