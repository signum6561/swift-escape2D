using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chicken : GroundedEnemy
{
    [SerializeField] private Transform playerDetect;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Vector2 playerDetectRange;
    private readonly float detectionDelay = 0.1f;
    private bool isPlayerInRange;
    private Transform playerPos;
    private float distance;
    protected override void Start()
    {
        base.Start();
        InitializeState(State.Idle);
        StartCoroutine(DetectPlayerCo());
    }
    protected override void IdleUpdate()
    {
        base.IdleUpdate();
        if (isPlayerInRange)
        {
            HandleFlip(playerPos.position.x - transform.position.x < 0 ? -1 : 1);
            wallDetected = CheckWall();
            ledgeDetected = CheckLedge();
            if (!wallDetected && ledgeDetected)
            {
                SwitchState(State.Move);
            }
        }
    }
    protected override void MoveUpdate()
    {
        base.MoveUpdate();
        if (isPlayerInRange)
        {
            distance = Vector2.Distance(transform.position, new Vector2(playerPos.position.x, transform.position.y));
            HandleFlip(playerPos.position.x < transform.position.x ? -1 : 1);
            wallDetected = CheckWall();
            ledgeDetected = CheckLedge();
            if (distance < 0.5f || wallDetected || !ledgeDetected)
            {
                SwitchState(State.Idle);
            }
            else
            {
                SetVelocityX(movementVelocity * FlipX);
            }
        }
        else
        {
            SwitchState(State.Idle);
        }

    }
    private IEnumerator DetectPlayerCo()
    {
        isPlayerInRange = PlayerDetect();
        playerPos = isPlayerInRange ? PlayerDetectPos().transform : null;
        yield return new WaitForSeconds(detectionDelay);
        StartCoroutine(DetectPlayerCo());
    }
    private Collider2D PlayerDetectPos()
    {
        return Physics2D.OverlapBox(playerDetect.position, playerDetectRange, 0f, playerMask);
    }
    private bool PlayerDetect()
    {
        return Physics2D.OverlapBox(playerDetect.position, playerDetectRange, 0f, playerMask);
    }
    protected override void DeadEnter()
    {
        base.DeadEnter();
        StopAllCoroutines();
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        if (playerDetect != null)
        {
            Gizmos.DrawWireCube(playerDetect.position, playerDetectRange);
        }
    }
}
