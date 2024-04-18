using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Progess,
    Lose,
    Finish
}

public class GameManager : MonoBehaviour
{
    public GameState CurrentGameState { get; private set; }
    public static GameManager Instance { get; private set; }
    public static event Action<GameState> OnGameStateChanged;
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
    private void Start()
    {
        StartCoroutine(EnterLevel());
    }
    public void SwitchGameState(GameState newState)
    {
        CurrentGameState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
    private IEnumerator EnterLevel()
    {
        yield return new WaitForSeconds(2f);
        SwitchGameState(GameState.Start);
    }
}


