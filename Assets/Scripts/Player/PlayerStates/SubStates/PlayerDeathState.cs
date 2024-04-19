using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.PlaySound("dead");
        stateMachine.SetAlive(false);
        player.Rb.bodyType = RigidbodyType2D.Kinematic;
        player.Col.enabled = false;
        player.ResetVelocity();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            player.gameObject.SetActive(false);
        }
    }
}
