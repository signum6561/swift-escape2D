using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player player = col.GetComponent<Player>();
            player.KnockbackState.SetTargetPos(transform.position);
            player.StateMachine.ChangeState(player.KnockbackState);
        }
    }
}
