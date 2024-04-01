using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowJump : Item
{
    protected override void OnHitPlayerEnter(Player player)
    {
        base.OnHitPlayerEnter(player);
        player.JumpState.IncreaseAmountOfJumps();
    }
}
