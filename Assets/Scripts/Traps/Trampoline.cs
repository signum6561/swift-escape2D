using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public Animator Anim { get; private set; }
    [SerializeField] private float jumpForce = 50f;
    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == (int)LayerIndex.Player)
        {
            IPushable pushable = col.GetComponent<IPushable>();
            pushable?.AddForce(new Vector2(0f, jumpForce));
            Anim.SetTrigger("jump");
        }
    }
}
