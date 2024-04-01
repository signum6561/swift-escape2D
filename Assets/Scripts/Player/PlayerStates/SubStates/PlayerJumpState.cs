using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilitiesState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        DescreaseAmountOfJumps();
        player.InAirState.SetIsJumping();
    }
    public bool CanJump()
    {
        return amountOfJumpsLeft > 0;
    }
    public void ResetAmountOfJumpLeft() => amountOfJumpsLeft = playerData.amountOfJumps;
    public void DescreaseAmountOfJumps() => amountOfJumpsLeft--;
    public void IncreaseAmountOfJumps() => amountOfJumpsLeft++;
}
