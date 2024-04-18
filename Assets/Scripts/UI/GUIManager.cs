using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private GameLogDisplay gameLog;
    [SerializeField] private GameObject pauseMenu;
    public static GUIManager Instance { get; private set; }
    private bool isPlayerComplete;
    private bool isPaused;
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
        GameManager.OnGameStateChanged += OnGameStateChanged;
        AchivementManager.OnAchivementTimeRemainUpdate += OnAchivementTimeRemainUpdate;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
        AchivementManager.OnAchivementTimeRemainUpdate -= OnAchivementTimeRemainUpdate;
    }
    public void HandlePauseMenu()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenu.SetActive(isPaused);
    }
    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Lose:
                isPlayerComplete = false;
                break;
            case GameState.Finish:
                isPlayerComplete = true;
                break;
        }
    }
    private void OnAchivementTimeRemainUpdate()
    {
        gameLog.DisplayAchivement(AchivementManager.Instance.AchivementData, isPlayerComplete);
        Invoke(nameof(DisplayGameLog), 1f);
    }
    private void DisplayGameLog() => gameLog.gameObject.SetActive(true);
}
