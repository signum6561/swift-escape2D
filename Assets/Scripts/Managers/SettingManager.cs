using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    public static Action<float> OnMusicVolumeChange;
    public static Action<float> OnSFXVolumeChange;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    public bool IsActive { get; private set; }
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void OpenSettingMenu()
    {
        settingMenu.SetActive(true);
        IsActive = true;
    }
    public void CloseSettingMenu()
    {
        settingMenu.SetActive(false);
        IsActive = false;
    }
    public void SFXVolumeSlider()
    {
        OnSFXVolumeChange?.Invoke(sfxSlider.value);
    }
    public void MusicVolumeSlider()
    {
        OnMusicVolumeChange?.Invoke(musicSlider.value);
    }

}
