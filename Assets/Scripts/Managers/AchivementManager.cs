using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    public static AchivementManager Instance { get; private set; }
    private int kills;
    private int score;
    private int remainTime;
    private int apple;
    private int banana;
    private int melon;
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
        Fruit.OnCollected += AddFruit;
    }
    private void OnDisable()
    {
        Enemy.OnDead -= AddKill;
        Scoreable.OnScoreChanged -= AddScore;
        Timer.OnTimerStop -= GetTimeRemain;
        Fruit.OnCollected -= AddFruit;
    }
    private void AddKill() => kills++;
    private void AddScore(int value) => score += value;
    private void GetTimeRemain(int value) => remainTime = value;
    private void AddFruit(ItemType fruitType)
    {
        switch (fruitType)
        {
            case ItemType.Apple:
                apple++;
                break;
            case ItemType.Banana:
                banana++;
                break;
            case ItemType.Melon:
                melon++;
                break;
        }
    }
    public int GetBonus()
    {
        return remainTime * 100;
    }
    public int GetFinalScore()
    {
        return score + GetBonus();
    }
}
