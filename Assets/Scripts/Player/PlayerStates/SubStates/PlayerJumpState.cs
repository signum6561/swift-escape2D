using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilitiesState
{
    private int amountOfJumpsLeft;
    private bool isDoubleJump;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }
    public override void Enter()
    {
        base.Enter();
        if (isDoubleJump)
        {
            player.Anim.SetTrigger("doubleJump");
        }
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        DescreaseAmountOfJumpsLeft();
        player.InAirState.SetIsJumping();
    }
    public bool CanJump()
    {
        Debug.Log(amountOfJumpsLeft);
        return amountOfJumpsLeft > 0;
    }
    public void ResetAmountOfJumpLeft() => amountOfJumpsLeft = playerData.amountOfJumps;
    public void DescreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
