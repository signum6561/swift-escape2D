using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCharacterListSO", menuName = "Data/CharactersList")]
public class CharacterListSO : ScriptableObject
{
    public Character[] charactersList;
    public int CharacterCount
    {
        get { return charactersList.Length; }
    }
    public Character GetCharacter(int characterIndex) => charactersList[characterIndex];
}
