using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainRender : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public void DrawLineChain(Vector2 startPos, Vector2 endPos)
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        // float distance = Vector2.Distance(startPos, endPos);
        // spriteRenderer.size = new Vector2((int)distance, 0.5f);
        // transform.position = Vector2.Lerp(startPos, endPos, 0.5f);
        // transform.right = endPos - startPos;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        float distance = Vector2.Distance(startPos, endPos);
        spriteRenderer.size = new Vector2((int)distance, 0.5f);
        spriteRenderer.transform.position += new Vector3(distance / 2, 0f, 0f);
        transform.right = endPos - startPos;
    }

    // public void DrawLineChainAlter(Vector2 startPos, Vector2 endPos)
    // {
    //     spriteRenderer = GetComponent<SpriteRenderer>();
    //     float distance = Vector2.Distance(startPos, endPos);
    //     spriteRenderer.size = new Vector2((int)distance, 0.5f);
    //     transform.position += new Vector3(distance / 2, 0f, 0f);
    //     transform.right = endPos - startPos;
    // }

}
