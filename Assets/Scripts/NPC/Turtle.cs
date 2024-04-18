using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    [SerializeField] private float attackDuration = 1f;
    private bool isSpikeOn;
    protected override void Start()
    {
        base.Start();
        InitializeState(State.Attack);
        StartCoroutine(SpikeCo());
    }
    protected override void IdleEnter()
    {
        base.IdleEnter();
    }
    protected override void AttackEnter()
    {
        base.AttackEnter();
    }
    protected override void DeadEnter()
    {
        StopAllCoroutines();
        base.DeadEnter();
    }
    public override void TakeDamage(int damage)
    {
        if (!isSpikeOn)
        {
            base.TakeDamage(damage);
        }
    }
    public void SpikeIn() => isSpikeOn = false;
    public void SpikeOut() => isSpikeOn = true;

    private IEnumerator SpikeCo()
    {
        SwitchState(State.Attack);
        yield return new WaitForSeconds(attackDuration);
        SwitchState(State.Idle);
        yield return new WaitForSeconds(attackDuration);
        StartCoroutine(SpikeCo());
    }
}
