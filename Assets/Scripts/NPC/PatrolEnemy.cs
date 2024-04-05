using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Enemy
{
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float movementVelocity = 3f;
    bool ledgeDetected;
    bool wallDetected;
    private Coroutine flipCo;
    protected override void Start()
    {
        base.Start();
        SwitchState(State.Move);
    }
    protected override void IdleEnter()
    {
        base.IdleEnter();
        SetVelocityX(0);
    }
    protected override void MoveUpdate()
    {
        base.MoveUpdate();
        ledgeDetected = CheckLedge();
        wallDetected = CheckWall();
        if (ledgeDetected && !wallDetected)
        {
            SetVelocityX(movementVelocity * FlipX);
        }
        else flipCo ??= StartCoroutine(FlipCo());
    }

    private IEnumerator FlipCo()
    {
        SwitchState(State.Idle);
        yield return new WaitForSeconds(waitTime);
        HandleFlip(-FlipX);
        flipCo = null;
        SwitchState(State.Move);
    }
}
