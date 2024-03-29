using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 8f;
    [Header("Jump State")]
    public float jumpVelocity = 10f;
    public int amountOfJumps = 1;
    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultipler = 0.5f;
    public float clampFallSpeed = 15f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayerMask;
}
