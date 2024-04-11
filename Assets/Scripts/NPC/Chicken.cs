using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : GroundedEnemy
{
    [SerializeField] private Transform playerDetect;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Vector2 playerDetectRange;

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireCube(playerDetect.position, playerDetectRange);
    }
}
