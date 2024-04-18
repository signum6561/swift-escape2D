using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Item
{
    public static event Action OnCollected;
    private void OnDisable()
    {
        OnCollected?.Invoke();
    }
}
