using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesState : PlayerState
{
    protected bool isAbilityDone;
    private bool isGrounded;
    public PlayerAbilitiesState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        isAbilityDone = false;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            if (isGrounded && player.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
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
}
