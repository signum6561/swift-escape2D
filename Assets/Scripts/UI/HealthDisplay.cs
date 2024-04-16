using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text ammountOfHeart;
    private int health;
    private void OnEnable()
    {
        PlayerHealth.OnHealthChanged += UpdateHeartNumber;
    }
    private void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= UpdateHeartNumber;
    }
    private void UpdateHeartNumber(int vaLue)
    {
        health = vaLue;
        ammountOfHeart.text = "x" + health.ToString();
    }
}
