using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : GroundedEnemy
{
    [SerializeField] private Transform shootPos;
    [SerializeField] private Transform playerDetect;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Vector2 playerDetectRange;
    [SerializeField] private float attackDuration = 0.5f;
    private readonly float detectionDelay = 0.1f;
    private bool isPlayerInRange;
    private bool isAttackFinished;
    private bool isCooling;
    protected override void Start()
    {
        base.Start();
        InitializeState(State.Idle);
        StartCoroutine(DetectPlayerCo());
    }
    protected override void IdleUpdate()
    {
        base.IdleUpdate();
        if (isPlayerInRange && !isCooling)
        {
            SwitchState(State.Attack);
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
        return Physics2D.OverlapBox(playerDetect.position, playerDetectRange, 0f, playerMask);
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
        GameObject bullet = PoolManager.Instance.GetObject(ObjectType.BulletPlant);
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
            Gizmos.DrawWireCube(playerDetect.position, playerDetectRange);
        }
    }
}
