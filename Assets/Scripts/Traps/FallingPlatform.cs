using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D col;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }
    private IEnumerator DeactiveAfter()
    {
        yield return new WaitForSeconds(duration);
        anim.SetTrigger("hit");
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)LayerIndex.Player)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.StateMachine.CurrentState is PlayerGroundedState)
            {
                StartCoroutine(DeactiveAfter());
            }
        }
    }
}
