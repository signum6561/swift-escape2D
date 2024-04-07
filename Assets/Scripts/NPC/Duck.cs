using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : GroundedEnemy
{
    [SerializeField] private float clampVelocity = 10f;
    [SerializeField] private float jumpVelocity = 20f;
    [SerializeField] private float jumpDuration = 0.5f;
    [SerializeField] private Transform heightCheck;
    [SerializeField] private float distanceHeightCheck = 3f;
    private bool isJumpEnterFinish;
    private float startJumpTime;
    private float finalMovementVelocity;
    private bool alterJump;
    protected override void Start()
    {
        base.Start();
        SwitchState(State.Idle);
    }
    protected override void IdleEnter()
    {
        base.IdleEnter();
        StartCoroutine(NextMove());
    }
    private IEnumerator NextMove()
    {
        yield return new WaitForSeconds(jumpDuration);
        if (CheckHeight())
        {
            StartFlipCoroutine();
        }
        else
        {
            SwitchState(State.Move);
        }
    }
    protected override void MoveUpdate()
    {
        base.MoveUpdate();
        if (isJumpEnterFinish)
        {
            isJumpEnterFinish = false;
            SwitchState(State.Attack);
        }
    }
    protected override void AttackEnter()
    {
        base.AttackEnter();
        SetVelocityY(jumpVelocity);
        if (!CheckWall())
        {
            finalMovementVelocity = Random.Range(movementVelocity, clampVelocity);
            SetVelocityX(finalMovementVelocity * FlipX);
        }
        else
        {
            alterJump = true;
        }
        startJumpTime = Time.time;
    }
    protected override void AttackUpdate()
    {
        base.AttackUpdate();
        if (Time.time > startJumpTime + 0.2f)
        {
            SwitchState(State.InAir);
        }
    }
    protected override void InAirUpdate()
    {
        base.InAirUpdate();
        Anim.SetFloat("velocityX", CurrentVelocity.x);
        Anim.SetFloat("velocityY", CurrentVelocity.y);
        if (CheckGround() && CurrentVelocity.y <= 0.01f)
        {
            SwitchState(State.Idle);
        }
        else if (alterJump)
        {
            alterJump = false;
            SetVelocityX(3f * FlipX);
        }
    }
    private bool CheckHeight()
    {
        return Physics2D.Raycast(heightCheck.position, Vector2.right * FlipX, distanceHeightCheck, wallLayerMask);
    }
    public void TriggerJumpEnterFinish() => isJumpEnterFinish = true;
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        if (heightCheck != null)
        {
            Gizmos.DrawLine(heightCheck.position, heightCheck.position + (Vector3)(FlipX * distanceHeightCheck * Vector2.right));
        }
    }
}
