using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerDoubleJumpState DoubleJumpState { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    public Animator Anim { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;
    private int flipX;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private PlayerData playerData;
    public void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StateMachine.initialize(IdleState);
    }
    private void Update()
    {
        CurrentVelocity = Rb.velocity;
        StateMachine.currentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }
    private void SetWorkspace(float x, float y)
    {
        workspace.Set(x, y > -playerData.clampFallSpeed ? y : -playerData.clampFallSpeed);
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.groundLayerMask);
    }
    public void HandleFlip(int inputX)
    {
        if (inputX != 0 && inputX != flipX)
        {
            flipX = inputX;
            SpriteRenderer.flipX = (flipX != 1);
        }
    }
    public void TriggerAnimationEnter() => StateMachine.currentState.TriggerAnimationEnter();
    public void TriggerAnimationExit() => StateMachine.currentState.TriggerAnimationExit();
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
    }
}
