using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSoundListSO", menuName = "Data/SoundList")]
public class SoundSO : ScriptableObject
{
    public Sound[] soundList;
    public int ListCount
    {
        get { return soundList.Length; }
    }
    public Sound GetSoundByName(string soundName)
    {
        return Array.Find(soundList, e => e.soundName == soundName);
    }
}
