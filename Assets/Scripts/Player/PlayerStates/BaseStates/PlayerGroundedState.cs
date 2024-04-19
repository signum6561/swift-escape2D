using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerGroundedState : PlayerState
{
    protected int inputX;
    private bool jumpInput;
    private bool isGrounded;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }
    public override void HandleChecks()
    {
        base.HandleChecks();
        isGrounded = player.CheckGround();
    }
    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpLeft();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        inputX = player.InputHandler.MoveInputX;
        jumpInput = player.InputHandler.JumpInput;
        if (jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            player.PlaySound("jump");
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
