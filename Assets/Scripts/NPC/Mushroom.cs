using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : GroundedEnemy
{
    protected override void Start()
    {
        base.Start();
        InitializeState(State.Idle);
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
        else
        {
            StartFlipCoroutine();
        };
    }
}
