using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerTxt;
    public int TimeRemain { get; private set; }
    public bool IsTimeOut { get; private set; }
    private int minutes;
    private int seconds;
    private Coroutine timerCo;
    public void StartTimer(int timeRemain)
    {
        TimeRemain = timeRemain;
        UpdateTimer();
        timerCo = StartCoroutine(TimerCo());
    }
    private IEnumerator TimerCo()
    {
        while (!IsTimeOut)
        {
            yield return new WaitForSeconds(1);
            TimeRemain--;
            UpdateTimer();
            if (TimeRemain <= 0)
            {
                IsTimeOut = true;
            }
        }
    }
    public void UpdateTimer()
    {
        minutes = (int)TimeRemain / 60;
        seconds = (int)TimeRemain % 60;
        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void StopTimer()
    {
        StopCoroutine(timerCo);
        timerCo = null;
    }
}
