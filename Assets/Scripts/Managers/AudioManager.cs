using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Player,
    UI,
    Other
}
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private SoundSO playerSFX;
    [SerializeField] private SoundSO uiSFX;
    [SerializeField] private SoundSO otherSFX;
    [SerializeField] private SoundSO music;
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeMusicVolume(float value)
    {
        musicSource.volume = value;
    }
    public void ChangeSFXVolume(float value)
    {
        sfxSource.volume = value;
    }
    public void PlaySFX(string name, SoundType soundType)
    {
        Sound s = null;
        switch (soundType)
        {
            case SoundType.Player:
                s = playerSFX.GetSoundByName(name);
                break;
            case SoundType.UI:
                s = uiSFX.GetSoundByName(name);
                break;
            case SoundType.Other:
                s = otherSFX.GetSoundByName(name);
                break;
        }

        if (s != null)
        {
            sfxSource.PlayOneShot(s.audioClip);
        }
    }
    public void PlayMusic(string name)
    {
        Sound s = music.GetSoundByName(name);
        if (s != null)
        {
            musicSource.clip = s.audioClip;
            musicSource.Play();
        }
    }
    public void StopMusic() => musicSource.Stop();
}
