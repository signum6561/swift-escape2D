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
    private int direction;
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
            direction = playerPos.position.x < transform.position.x ? -1 : 1;
            distance = Vector2.Distance(transform.position, new Vector2(playerPos.position.x, transform.position.y));
            HandleFlip(direction);
            wallDetected = CheckWall();
            ledgeDetected = CheckLedge();
            if (!wallDetected && ledgeDetected && distance > 0.5f)
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
            wallDetected = CheckWall();
            ledgeDetected = CheckLedge();
            HandleFlip(direction);
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
        StopAllCoroutines();
        base.DeadEnter();
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
