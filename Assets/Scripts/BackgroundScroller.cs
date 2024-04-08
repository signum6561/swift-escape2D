using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private Vector2 direction;
    [SerializeField] private CompositeCollider2D boundary;
    [SerializeField] private Camera mainCamera;
    void FixedUpdate()
    {
        img.uvRect = new Rect(img.uvRect.position + direction * Time.fixedDeltaTime, img.uvRect.size);
        transform.position = mainCamera.transform.position;
    }
}
