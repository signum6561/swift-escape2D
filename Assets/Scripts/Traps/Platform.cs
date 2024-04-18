using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == (int)LayerIndex.Player)
        {
            col.transform.SetParent(this.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == (int)LayerIndex.Player)
        {
            col.transform.SetParent(null);
        }
    }
}
