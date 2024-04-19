using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static event Action<int> OnTimerStop;
    [SerializeField] private int startTimeRemain;
    [SerializeField] private Timer timer;
    [SerializeField] private GameLogDisplay gameLog;
    [SerializeField] private GameObject pauseMenu;
    public static GUIManager Instance { get; private set; }
    private bool isPaused;
    private GameObject currentPanel;
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
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
    public void HandlePauseMenu()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenu.SetActive(isPaused);
    }
    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Finish && !timer.IsTimeOut)
        {
            timer.StopTimer();
            OnTimerStop?.Invoke(timer.TimeRemain);
            HandleGameLog(true);
        }
        else if (gameState == GameState.Lose)
        {
            timer.StopTimer();
            OnTimerStop?.Invoke(0);
            HandleGameLog(false);
        }
        else if (gameState == GameState.Start && startTimeRemain > 0)
        {
            timer.StartTimer(startTimeRemain);
        }
    }
    private void HandleGameLog(bool isPlayerComplete)
    {
        gameLog.DisplayAchivement(AchivementManager.Instance.AchivementData, isPlayerComplete);
        Invoke(nameof(DisplayGameLog), 1f);
    }
    private void DisplayGameLog() => gameLog.gameObject.SetActive(true);
}
