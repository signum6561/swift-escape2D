using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f;

    [SerializeField] private float duration = 2f;
    private bool isFireOn;
    public Animator Anim { get; private set; }
    [SerializeField] private BoxCollider2D TriggerArea;

    private Coroutine fireCo;
    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    public void SetFire() => isFireOn = true;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == (int)LayerIndex.Player)
        {
            fireCo ??= StartCoroutine(FireCo());
            if (isFireOn)
            {
                IDamageable damageable = col.GetComponent<IDamageable>();
                damageable?.TakeDamage(1);
            }
        }
    }

    private IEnumerator FireCo()
    {
        Anim.SetTrigger("activate");
        TriggerArea.enabled = false;
        yield return new WaitForSeconds(delay);
        Anim.SetTrigger("onFire");
        isFireOn = true;
        TriggerArea.enabled = true;
        yield return new WaitForSeconds(duration);
        Anim.SetTrigger("deactivate");
        isFireOn = false;
        fireCo = null;
    }

}
