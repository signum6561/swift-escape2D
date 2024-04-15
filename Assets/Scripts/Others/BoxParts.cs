using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxParts : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector2 pushForce;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Rigidbody2D Rb { get; private set; }

    private float torque;
    private float randX;
    private float randY;
    private Vector2 finalForce;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private void OnEnable()
    {
        if (Rb == null)
        {
            Rb = GetComponent<Rigidbody2D>();
        }
        originalPos = transform.position;
        originalRot = transform.rotation;
        spriteRenderer.enabled = true;
        randX = Random.Range(direction.x * pushForce.x, direction.x * (pushForce.x + 10f));
        randY = Random.Range(direction.y * pushForce.y, direction.y * (pushForce.y + 10f));
        torque = Random.Range(-50f, 50f);
        finalForce.Set(randX, randY);
        Rb.AddForce(finalForce, ForceMode2D.Impulse);
        Rb.AddTorque(torque);
        StartCoroutine(FlashEffect());
    }
    private IEnumerator FlashEffect()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(FlashCo());
        yield return new WaitForSeconds(1f);
        StopAllCoroutines();
        spriteRenderer.enabled = false;
    }
    private IEnumerator FlashCo()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        StartCoroutine(FlashCo());
    }
    private void OnDisable()
    {
        transform.SetPositionAndRotation(originalPos, originalRot);
    }
}
