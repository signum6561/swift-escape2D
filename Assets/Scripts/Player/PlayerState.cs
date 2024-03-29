using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;
    protected bool isAnimationFinished;
    private readonly string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        HandleChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
        isAnimationFinished = false;
    }
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        HandleChecks();
    }
    public virtual void HandleChecks() { }
    public virtual void TriggerAnimationEnter() { }
    public virtual void TriggerAnimationExit() => isAnimationFinished = true;

}
