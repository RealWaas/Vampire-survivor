using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] Button mainButton;
    [SerializeField] TMP_Text characterName;
    [SerializeField] Image weaponSprite;
    [SerializeField] CharacterDataSO character;

    private void Awake()
    {
        mainButton.onClick.AddListener(OnCLicked);
        
        SetCharacter(character);
    }
    private void OnDestroy()
    {
        mainButton.onClick.RemoveListener(OnCLicked);
    }

    private void SetCharacter(CharacterDataSO _character)
    {
        character = _character;
        characterName.text = character.entityName;
        weaponSprite.sprite = character.startingWeapon.weaponSprite;
    }


    private void OnCLicked()
    {
        if (character == null) return;

        CharacterStats.instance.SetCharacterStats(character);
    }
}