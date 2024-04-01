using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected Animator anim;
    protected bool isAnimationFinished;
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player player = col.GetComponent<Player>();
            OnHitPlayerEnter(player);
        }
    }
    protected virtual void OnHitPlayerEnter(Player player)
    {
        anim.SetTrigger("hit");
    }
    public void TriggerAnimationFinished()
    {
        isAnimationFinished = true;
        gameObject.SetActive(false);
    }
}
