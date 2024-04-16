using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : GroundedEnemy
{
    [SerializeField] private Transform shootPos;
    [SerializeField] private Transform playerDetect;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float playerDetectRange = 10f;
    [SerializeField] private float attackDuration = 0.5f;
    private readonly float detectionDelay = 0.1f;
    private bool isPlayerInRange;
    private bool isAttackFinished;
    private bool isCooling;
    private float startWait;
    protected override void Start()
    {
        base.Start();
        InitializeState(State.Move);
        StartCoroutine(DetectPlayerCo());
    }
    protected override void MoveUpdate()
    {
        base.MoveUpdate();
        ledgeDetected = CheckLedge();
        wallDetected = CheckWall();
        if (ledgeDetected && !wallDetected)
        {
            if (isPlayerInRange)
            {
                SwitchState(State.Idle);
            }
            else
            {
                SetVelocityX(movementVelocity * FlipX);
            }
        }
        else
        {
            SwitchState(State.Idle);
        };
    }
    protected override void IdleEnter()
    {
        base.IdleEnter();
        startWait = Time.time;
    }
    protected override void IdleUpdate()
    {
        base.IdleUpdate();
        if (isPlayerInRange && !isCooling)
        {
            SwitchState(State.Attack);
        }
        else if (Time.time > startWait + waitTime)
        {
            ledgeDetected = CheckLedge();
            wallDetected = CheckWall();
            if (!ledgeDetected || wallDetected)
            {
                HandleFlip(-FlipX);
                SwitchState(State.Move);
            }
            else
            {
                SwitchState(State.Move);
            }
        }
    }
    protected override void AttackEnter()
    {
        base.AttackEnter();
        isAttackFinished = false;
    }
    protected override void AttackUpdate()
    {
        base.AttackUpdate();
        if (isAttackFinished)
        {
            StartCoroutine(AttackCoolDown());
            SwitchState(State.Idle);
        }
    }

    private bool PlayerDetect()
    {
        return Physics2D.Raycast(playerDetect.position, Vector2.right * FlipX, playerDetectRange, playerMask);
    }
    private IEnumerator AttackCoolDown()
    {
        isCooling = true;
        yield return new WaitForSeconds(attackDuration);
        isCooling = false;
    }
    private IEnumerator DetectPlayerCo()
    {
        isPlayerInRange = PlayerDetect();
        yield return new WaitForSeconds(detectionDelay);
        StartCoroutine(DetectPlayerCo());
    }
    public void TriggerAttackFinished()
    {
        isAttackFinished = true;
        GameObject bullet = PoolManager.Instance.GetObject(ObjectType.BulletTrunk);
        bullet.transform.position = shootPos.position;
        bullet.transform.right = shootPos.right;
        bullet.SetActive(true);
    }
    protected override void DeadEnter()
    {
        StopAllCoroutines();
        base.DeadEnter();
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.blue;
        if (playerDetect != null)
        {
            Gizmos.DrawLine(playerDetect.position, playerDetect.position + (Vector3)(FlipX * playerDetectRange * Vector2.right));
        }
    }
}
