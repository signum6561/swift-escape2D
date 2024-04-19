using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void OnMenuButtonClick() => LevelManager.Instance.LoadScene("MainMenu");
    public void OnRestartButtonClick() => LevelManager.Instance.ReloadScene();
    public void OnOptionButtonClick()
    {

        SettingManager.Instance.OpenSettingMenu();
    }

}
