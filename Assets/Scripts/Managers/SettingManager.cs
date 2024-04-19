using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] private GameObject settingMenu;
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void OpenSettingMenu() => settingMenu.SetActive(true);
}
