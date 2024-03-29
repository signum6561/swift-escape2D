using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public int MoveInputX { get; private set; }
    public int MoveInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;
    private float jumpStartTime;
    private void Update()
    {
        CheckJumpInputHoldTime();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        MoveInputX = (int)(MovementInput * Vector2.right).normalized.x;
        MoveInputY = (int)(MovementInput * Vector2.up).normalized.y;
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }
    public void UseJumpInput() => JumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
