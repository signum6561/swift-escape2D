using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTxt;
    private int score;
    private void OnEnable()
    {
        Scoreable.OnScoreChanged += AddAndUpdateScoreText;
    }
    private void OnDisable()
    {
        Scoreable.OnScoreChanged -= AddAndUpdateScoreText;
    }
    private void AddAndUpdateScoreText(int vaLue)
    {
        score += vaLue;
        scoreTxt.text = string.Format("{0:00000000}", score);
    }
}
