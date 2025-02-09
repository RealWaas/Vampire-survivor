using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterSelectButton : MonoBehaviour
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

    /// <summary>
    /// Fill all the spaces with the character datas.
    /// </summary>
    /// <param name="_character">given character</param>
    private void SetCharacter(CharacterDataSO _character)
    {
        character = _character;
        characterName.text = character.entityName;
        weaponSprite.sprite = character.startingWeapon.itemSprite;
    }

    /// <summary>
    /// Set the character as selected and show its stats.
    /// </summary>
    private void OnCLicked()
    {
        if (character == null) return;

        UI_CharacterSelection.instance.SetCharacterDescription(character);
    }
}