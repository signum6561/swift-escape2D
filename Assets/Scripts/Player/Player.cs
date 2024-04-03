using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerKnockbackState KnockbackState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }

    public PlayerInputHandler InputHandler { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public SpriteRenderer Spr { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public int FlipX { get; private set; }
    private Vector2 workspace;
    private bool isDamageable;
    private Coroutine flashCo;
    public int Health { get; private set; }
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private PlayerData playerData;
    public void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        KnockbackState = new PlayerKnockbackState(this, StateMachine, playerData, "hit");
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "dead");

    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rb = GetComponent<Rigidbody2D>();
        Spr = GetComponent<SpriteRenderer>();
        isDamageable = true;
        FlipX = 1;
        Health = playerData.health;
        StateMachine.initialize(IdleState);
    }
    private void Update()
    {
        CurrentVelocity = Rb.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    private void SetWorkspace(float x, float y)
    {
        workspace.Set(x, y > -playerData.clampFallSpeed ? y : -playerData.clampFallSpeed);
    }
    public void SetVelocityX(float velocity)
    {
        // Debug.Log($"{origin} set velocityX => {velocity}");
        SetWorkspace(velocity, CurrentVelocity.y);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityY(float velocity)
    {
        SetWorkspace(CurrentVelocity.x, velocity);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void ResetVelocity()
    {
        workspace.Set(0f, 0f);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.groundLayerMask);
    }
    public bool CheckWall()
    {
        // Debug.DrawRay(wallCheck.position, Vector2.right * flipX, Color.red, playerData.wallCheckDistance);
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FlipX, playerData.wallCheckDistance, playerData.groundLayerMask);
    }
    public void HandleFlip(int inputX)
    {
        if (inputX != 0 && inputX != FlipX)
        {
            FlipX *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    public void SetImmortal()
    {
        if (flashCo != null)
            return;
        flashCo = StartCoroutine(ImmortalCo());
    }
    private IEnumerator ImmortalCo()
    {
        StartCoroutine(Flash());
        yield return new WaitForSeconds(1f);
        StopAllCoroutines();
        flashCo = null;
        isDamageable = true;
    }
    private IEnumerator Flash()
    {
        Spr.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Spr.enabled = true;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Flash());
    }
    public void TriggerAnimationEnter() => StateMachine.CurrentState.TriggerAnimationEnter();
    public void TriggerAnimationExit() => StateMachine.CurrentState.TriggerAnimationExit();
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(FlipX * playerData.wallCheckDistance * Vector2.right));
    }

    public void TakeDamage(int damage)
    {
        if (isDamageable)
        {
            Health--;
            isDamageable = false;
            if (Health <= 0)
            {
                StateMachine.ChangeState(DeathState);
            }
            else
            {
                StateMachine.ChangeState(KnockbackState);
            }
        }
    }
}
