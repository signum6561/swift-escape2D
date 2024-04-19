using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private CharacterListSO charactersSO;
    [SerializeField] private CharacterSelectionDisplay characterDisplay;
    [SerializeField] private LevelSelectionDisplay levelDisplay;
    [SerializeField] private TMP_Text title;
    [SerializeField] private int numberOfLevel;
    private GameObject currentMenu;
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
        currentMenu = characterDisplay.gameObject;
        characterDisplay.InitializeSelection(charactersSO);
        levelDisplay.GenerateLevelButton(numberOfLevel);
    }
    public void OnPlayButtonClick()
    {
        AudioManager.Instance.PlaySFX("playClick", SoundType.UI);
        GameManager.Instance.SwitchGameState(GameState.LevelSelection);
        LevelManager.Instance.PlayerPrefab = characterDisplay.GetSelectedCharacter();
    }
    public void OnBackButtonClick()
    {
        GameManager.Instance.SwitchGameState(GameState.CharacterSelection);
    }
    public void OnOptionButtonClick()
    {
        SettingManager.Instance.OpenSettingMenu();
    }
    private void SwitchMenu(GameObject menu)
    {
        currentMenu.SetActive(false);
        currentMenu = menu;
        currentMenu.SetActive(true);
    }
    private void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.CharacterSelection:
                title.text = "SWIFT ESCAPE 2D";
                SwitchMenu(characterDisplay.gameObject);
                break;
            case GameState.LevelSelection:
                title.text = "LEVEL SELECT";
                SwitchMenu(levelDisplay.gameObject);
                break;
        }
    }
}
