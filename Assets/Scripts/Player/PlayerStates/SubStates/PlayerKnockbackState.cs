using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerKnockbackState : PlayerState
{
    // private Coroutine knockRoutine;
    private Vector2 targetPos;
    public PlayerKnockbackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        KnockBack();
    }
    public void SetTargetPos(Vector2 targetPos) => this.targetPos = targetPos;
    public void KnockBack()
    {


    }
    private IEnumerator KnockCo(float knockbackTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        // knockRoutine = null;
        stateMachine.ChangeState(player.IdleState);
    }

}
