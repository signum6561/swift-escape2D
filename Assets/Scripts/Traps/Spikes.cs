using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == (int)LayerIndex.Player)
        {
            IDamageable damageable = col.GetComponent<IDamageable>();
            damageable?.TakeDamage(1);
        }
    }
}
