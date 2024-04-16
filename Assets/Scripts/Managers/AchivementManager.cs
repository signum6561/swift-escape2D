using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    public static AchivementManager Instance { get; private set; }
    private int kills;
    private int score;
    private int remainTime;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void OnEnable()
    {
        Enemy.OnDead += AddKill;
        Scoreable.OnScoreChanged += AddScore;
        Timer.OnTimerStop += GetTimeRemain;
    }
    private void OnDisable()
    {
        Enemy.OnDead -= AddKill;
        Scoreable.OnScoreChanged -= AddScore;
        Timer.OnTimerStop -= GetTimeRemain;
    }
    private void AddKill() => kills++;
    private void AddScore(int value) => score += value;
    private void GetTimeRemain(int value) => remainTime = value;
    public int GetBonus()
    {
        return remainTime * 100;
    }
    public int GetFinalScore()
    {
        return score + GetBonus();
    }
}
