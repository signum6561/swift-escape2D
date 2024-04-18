using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreable : MonoBehaviour
{
    [SerializeField] private int points;
    public static event Action<int> OnScoreChanged;
    private void OnDisable()
    {
        OnScoreChanged?.Invoke(points);
    }
}
