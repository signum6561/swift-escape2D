using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : FlyingEnemy
{
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private float radiusWallCheck = 1f;
    [SerializeField] private float movementVelocity = 3f;
    [SerializeField] private float waitTime = 3f;

    private Coroutine flipCo;
    protected override void Start()
    {
        base.Start();
        SwitchState(State.Move);
    }
    protected override void IdleEnter()
    {
        base.IdleEnter();
        SetVelocityX(0);
    }
    protected override void MoveUpdate()
    {
        base.MoveUpdate();
        if (!CheckWall())
        {
            SetVelocityX(movementVelocity * FlipX);
        }
        else flipCo ??= StartCoroutine(FlipCo());
    }
    protected IEnumerator FlipCo()
    {
        SwitchState(State.Idle);
        yield return new WaitForSeconds(waitTime);
        HandleFlip(-FlipX);
        flipCo = null;
        if (IsAlive)
        {
            SwitchState(State.Move);
        }
    }
    public bool CheckWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, radiusWallCheck, wallLayerMask);
    }
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(wallCheck.position, radiusWallCheck);
    }
}
