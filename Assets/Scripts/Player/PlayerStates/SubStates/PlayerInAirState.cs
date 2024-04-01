using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private int inputX;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool isCoyoteTimeStart;
    private bool isTouchingWall;
    private bool isJumping;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void HandleChecks()
    {
        base.HandleChecks();
        isGrounded = player.CheckGround();
        isTouchingWall = player.CheckWall();
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        inputX = player.InputHandler.MoveInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;

        CheckJumpMutipler();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            if (!isTouchingWall)
            {
                player.Anim.SetTrigger("doubleJump");
            }
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && inputX == player.FlipX && player.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            player.HandleFlip(inputX);
            player.SetVelocityX(playerData.movementVelocity * inputX);

            player.Anim.SetFloat("velocityY", player.CurrentVelocity.y);
            player.Anim.SetFloat("velocityX", Mathf.Abs(player.CurrentVelocity.x));
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void CheckJumpMutipler()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y + playerData.variableJumpHeightMultipler);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }
    private void CheckCoyoteTime()
    {
        if (isCoyoteTimeStart && Time.time > startTime + playerData.coyoteTime)
        {
            isCoyoteTimeStart = false;
            player.JumpState.DescreaseAmountOfJumps();
        }
    }
    public void StartCoyoteTime() => isCoyoteTimeStart = true;
    public void SetIsJumping() => isJumping = true;
}
