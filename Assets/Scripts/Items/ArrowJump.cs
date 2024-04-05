using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowJump : Item
{
    protected override void OnHitPlayerEnter(Player player)
    {
        base.OnHitPlayerEnter(player);
        player.JumpState.SetAmountOfJumps(1);
        player.StateMachine.ChangeState(player.JumpState);

    }
}
