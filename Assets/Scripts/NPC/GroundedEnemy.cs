using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemy : Enemy
{
    [SerializeField] protected LayerMask groundLayerMask;
    [SerializeField] protected LayerMask wallLayerMask;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float distanceLedgeCheck = 0.5f;
    [SerializeField] private float distanceWallCheck = 0.5f;
    [SerializeField] private float radiusGroundCheck = 0.5f;

    [SerializeField] protected float waitTime = 2f;
    [SerializeField] protected float movementVelocity = 3f;
    protected bool ledgeDetected;
    protected bool wallDetected;
    private Coroutine flipCo;
    protected override void IdleEnter()
    {
        base.IdleEnter();
        ResetVelocity();
    }
    protected void StartFlipCoroutine()
    {
        flipCo ??= StartCoroutine(FlipCo());
    }
    protected IEnumerator FlipCo()
    {
        SwitchState(State.Idle);
        yield return new WaitForSeconds(waitTime);
        HandleFlip(-FlipX);
        flipCo = null;
        SwitchState(State.Move);
    }
    public bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, distanceLedgeCheck, groundLayerMask);
    }
    public bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FlipX, distanceWallCheck, wallLayerMask);
    }

    public bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, radiusGroundCheck, groundLayerMask);
    }
    public virtual void OnDrawGizmosSelected()
    {
        if (ledgeCheck != null)
        {
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(distanceLedgeCheck * Vector2.down));
        }
        if (wallCheck != null)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(FlipX * distanceWallCheck * Vector2.right));
        }
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, radiusGroundCheck);
        }
    }
}
