using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchivementManager : Singleton<AchivementManager>
{
    public Achivement AchivementData { get; private set; }
    private void Start()
    {
        AchivementData = new Achivement();
    }
    private void OnEnable()
    {
        Enemy.OnDead += AddKill;
        Scoreable.OnScoreChanged += AddScore;
        GUIManager.OnTimerStop += GetTimeRemain;
        Fruit.OnCollected += AddFruit;
        Diamond.OnCollected += AddDiamond;
    }
    private void OnDisable()
    {
        Enemy.OnDead -= AddKill;
        Scoreable.OnScoreChanged -= AddScore;
        GUIManager.OnTimerStop -= GetTimeRemain;
        Fruit.OnCollected -= AddFruit;
        Diamond.OnCollected -= AddDiamond;
    }
    private void AddKill() => AchivementData.kills++;
    private void AddScore(int value) => AchivementData.score += value;
    private void AddDiamond()
    {
        if (AchivementData.diamonds < 3)
        {
            AchivementData.diamonds++;
        }
    }
    private void GetTimeRemain(int value)
    {
        AchivementData.timeRemain = value;
    }
    private void AddFruit(ItemType fruitType)
    {
        switch (fruitType)
        {
            case ItemType.Apple:
                AchivementData.appleCount++;
                break;
            case ItemType.Banana:
                AchivementData.bananaCount++;
                break;
            case ItemType.Melon:
                AchivementData.melonCount++;
                break;
        }
    }
}
