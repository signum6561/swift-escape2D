using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action<int> OnTimerStop;
    [SerializeField] private TMP_Text timerTxt;
    [SerializeField] private int remainTime;
    private int minutes;
    private int seconds;
    private Coroutine timerCo;
    private bool isTimeOut;
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
    private void Start()
    {
        UpdateTimer();
    }
    private IEnumerator TimerCo()
    {
        while (!isTimeOut)
        {
            yield return new WaitForSeconds(1);
            remainTime--;
            UpdateTimer();
            if (remainTime <= 0)
            {
                isTimeOut = true;
            }
        }
    }
    private void UpdateTimer()
    {
        minutes = (int)remainTime / 60;
        seconds = (int)remainTime % 60;
        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Finish && !isTimeOut)
        {
            StopCoroutine(timerCo);
            timerCo = null;
            OnTimerStop?.Invoke(remainTime);
        }
        else if (gameState == GameState.Lose)
        {
            remainTime = 0;
            StopCoroutine(timerCo);
            timerCo = null;
            OnTimerStop?.Invoke(remainTime);
        }
        else if (gameState == GameState.Start && remainTime > 0)
        {
            timerCo = StartCoroutine(TimerCo());
        }
    }
}
