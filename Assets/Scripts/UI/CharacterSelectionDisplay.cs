using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSelectionDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private Animator anim;
    private int selectedOption;
    private CharacterListSO charactersSO;
    private int characterCount;
    private void UpdateCharacterDisplay(Character character)
    {
        characterName.text = character.name;
        anim.runtimeAnimatorController = character.animController;
    }
    public void InitializeSelection(CharacterListSO charactersSO)
    {
        characterCount = charactersSO.CharacterCount;
        this.charactersSO = charactersSO;
        UpdateCharacterDisplay(charactersSO.GetCharacter(0));
    }
    public GameObject GetSelectedCharacter() => charactersSO.GetCharacter(selectedOption).characterPrefab;

    public void NextCharacter()
    {
        selectedOption = (selectedOption + 1) % characterCount;
        UpdateCharacterDisplay(charactersSO.GetCharacter(selectedOption));
    }
    public void PrevCharacter()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = characterCount - 1;
        }
        UpdateCharacterDisplay(charactersSO.GetCharacter(selectedOption));
    }
}
