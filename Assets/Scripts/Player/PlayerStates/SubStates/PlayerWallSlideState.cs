using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityY(-playerData.wallSlideVelocity);
        if (jumpInput && isAnimationFinished)
        {
            player.InputHandler.UseJumpInput();
            player.JumpState.ResetAmountOfJumpLeft();
            stateMachine.ChangeState(player.JumpState);
        }

    }
}
