using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void HandleChecks()
    {
        base.HandleChecks();
    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);

    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.HandleFlip(inputX);
        player.SetVelocityX(playerData.movementVelocity * inputX);
        if (inputX == 0 && !isExistingState)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
