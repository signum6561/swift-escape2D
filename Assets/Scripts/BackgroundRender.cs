using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BackgroundRender : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private Sprite[] sprites;
    private void Start()
    {
        int range = sprites.Length;
        int randIndex = Random.Range(0, range);
        image.texture = sprites[randIndex].texture;
    }

}
