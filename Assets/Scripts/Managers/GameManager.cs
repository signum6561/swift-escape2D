using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    CharacterSelection,
    LevelSelection,
    Start,
    Progess,
    Lose,
    Finish
}

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentGameState { get; private set; }
    public static event Action<GameState> OnGameStateChanged;
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SwitchGameState(GameState newState)
    {
        CurrentGameState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
    public void StartEnterLevel()
    {
        StartCoroutine(EnterLevel());
    }
    private IEnumerator EnterLevel()
    {
        yield return new WaitForSeconds(1f);
        SwitchGameState(GameState.Start);
    }
}


