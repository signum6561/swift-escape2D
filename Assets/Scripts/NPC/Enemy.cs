using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform wallCheck;

    [SerializeField] private float distanceLedgeCheck = 0.5f;
    [SerializeField] private float distanceWallCheck = 0.5f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private LayerMask playerLayerMask;

    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public BoxCollider2D Col { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }
    public int FlipX { get; private set; }
    protected bool IsAlive;

    private float rotZ;
    public enum State
    {
        Idle,
        Move,
        Attack,
        Dead,
    };
    public State CurrentState { get; private set; }
    protected string currentAnim = "idle";
    public void SwitchState(State newState)
    {
        Anim.SetBool(currentAnim, false);
        CurrentState = newState;
        switch (CurrentState)
        {
            case State.Idle:
                currentAnim = "idle";
                IdleEnter();
                break;
            case State.Move:
                currentAnim = "move";
                MoveEnter();
                break;
            case State.Attack:
                currentAnim = "attack";
                AttackEnter();
                break;
            case State.Dead:
                currentAnim = "dead";
                DeadEnter();
                break;
        }
        Anim.SetBool(currentAnim, true);
    }
    protected virtual void Start()
    {
        LoadComponents();
        FlipX = transform.rotation.y < 0 ? -1 : 1;
        IsAlive = true;
    }
    protected virtual void LoadComponents()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Col = GetComponent<BoxCollider2D>();

    }
    protected virtual void Update()
    {
        switch (CurrentState)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Move:
                MoveUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;
            case State.Dead:
                DeadUpdate();
                break;
        }
    }
    protected virtual void FixedUpdate() { }
    protected virtual void IdleEnter() { }
    protected virtual void IdleUpdate() { }
    protected virtual void MoveEnter() { }
    protected virtual void MoveUpdate() { }
    protected virtual void AttackEnter() { }
    protected virtual void AttackUpdate() { }
    protected virtual void DeadEnter()
    {
        ResetVelocity();
        IsAlive = false;
        Col.enabled = false;
        Rb.AddForce(new Vector2(FlipX * 5f, 25f), ForceMode2D.Impulse);
        StartCoroutine(DeadCo());
    }
    protected virtual void DeadUpdate()
    {
        rotZ += 50f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
    protected IEnumerator DeadCo()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    public bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, distanceLedgeCheck, groundLayerMask);
    }
    public bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FlipX, distanceWallCheck, wallLayerMask);
    }
    public bool DetectPlayer()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FlipX, distanceWallCheck, playerLayerMask);
    }
    public void HandleFlip(int value)
    {
        if (value != 0 && value != FlipX)
        {
            FlipX *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    public void SetVelocityX(float velocity)
    {
        CurrentVelocity = new Vector2(velocity, CurrentVelocity.y);
        // CurrentVelocity.Set(velocity, CurrentVelocity.y);
        Rb.velocity = CurrentVelocity;
    }
    public void SetVelocityY(float velocity)
    {
        CurrentVelocity = new Vector2(CurrentVelocity.y, velocity);
        // CurrentVelocity.Set(CurrentVelocity.x, velocity);
        Rb.velocity = CurrentVelocity;
    }
    public void ResetVelocity()
    {
        CurrentVelocity = Vector2.zero;
        Rb.velocity = CurrentVelocity;
    }
    public void TakeDamage(int damage)
    {
        if (IsAlive)
        {
            health--;
            if (health <= 0)
            {
                SwitchState(State.Dead);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsAlive)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(1);
        }
    }
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(distanceLedgeCheck * Vector2.down));
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(FlipX * distanceWallCheck * Vector2.right));
    }
}
