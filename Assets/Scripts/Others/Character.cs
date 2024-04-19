using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string name;
    public AnimatorController animController;
    public GameObject characterPrefab;
}
