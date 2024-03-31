using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float movementVelocity = 8f;
    [Header("Jump")]
    public float jumpVelocity = 10f;
    public float doubleJumpVelocity = 10f;
    public int amountOfJumps = 1;

    [Header("In Air")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultipler = 0.5f;
    public float clampFallSpeed = 15f;
    [Header("Wall Slide")]
    public float wallSlideVelocity = 3f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask groundLayerMask;
}
