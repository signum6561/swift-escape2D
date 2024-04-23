using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : PlayerState, IKnockable
{
    private Vector2 targetPos;
    public PlayerKnockbackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.PlaySound("takeDamage");
        KnockBack(targetPos, playerData.knockbackForce, 1);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            player.ResetVelocity();
            stateMachine.ChangeState(player.IdleState);
        }
    }
    public void SetTargetPos(Vector2 targetPos) => this.targetPos = targetPos;
    public void KnockBack(Vector2 targetPos, float knockbackForce, int direction)
    {
        Vector2 angle = (Vector2)player.transform.position - targetPos;
        player.SetVelocity(knockbackForce, angle, direction);
        player.SetVelocityY(15f);
    }
}
