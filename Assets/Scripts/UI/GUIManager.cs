using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (!SettingManager.Instance.IsActive)
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
            pauseMenu.SetActive(isPaused);
        }
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
        StartCoroutine(DisplayGameLog(isPlayerComplete));
    }
    private IEnumerator DisplayGameLog(bool value)
    {
        yield return new WaitForSeconds(1f);
        gameLog.gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX(value ? "victory" : "gameOver", SoundType.UI);
    }
    public void OnMenuButtonClick() => LevelManager.Instance.LoadScene("MainMenu");
    public void OnRestartButtonClick() => LevelManager.Instance.ReloadScene();
    public void OnOptionButtonClick() => SettingManager.Instance.OpenSettingMenu();
    public void OnNextButtonClick() => LevelManager.Instance.NextLevel();
}
