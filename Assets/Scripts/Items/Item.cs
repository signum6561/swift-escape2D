using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Animator Anim { get; private set; }
    protected bool isAnimationFinished;
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == (int)LayerIndex.Player)
        {
            OnHitPlayerEnter(col);
        }
    }
    protected virtual void OnHitPlayerEnter(Collider2D col)
    {
        Anim.SetTrigger("hit");
    }
    public virtual void TriggerAnimationFinished()
    {
        isAnimationFinished = true;
        gameObject.SetActive(false);
    }
}
