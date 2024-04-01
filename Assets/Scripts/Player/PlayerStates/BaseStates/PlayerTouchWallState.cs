using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected int inputX;
    protected bool jumpInput;

    public PlayerTouchWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
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
        if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || inputX != player.FlipX)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }
    public override void TriggerAnimationEnter()
    {
        base.TriggerAnimationEnter();
    }
    public override void TriggerAnimationExit()
    {
        base.TriggerAnimationExit();
    }
    public override void HandleChecks()
    {
        base.HandleChecks();
        isGrounded = player.CheckGround();
        isTouchingWall = player.CheckWall();
    }

}
