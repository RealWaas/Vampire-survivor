using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterSelection : MonoBehaviour
{
    public static UI_CharacterSelection instance;
    private CharacterDataSO character;

    [SerializeField] private TMP_Text textArea;
    [SerializeField] private Button startButton;

    private void Awake()
    {
        instance = this;
        startButton.interactable = false;
        startButton.onClick.AddListener(OnCLicked);
    }
    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(OnCLicked);
    }

    /// <summary>
    /// Set the character description with its stats and select it.
    /// </summary>
    /// <param name="_data">selected character</param>
    public void SetCharacterDescription(CharacterDataSO _data)
    {
        startButton.interactable = true;
        character = _data;
        string charaDescription = "";

        //Infos
        charaDescription += $"Name : {character.entityName}<br>";
        charaDescription += $"Weapon : {character.startingWeapon.itemName}<br>";

        //Stats
        charaDescription += $"Health : {character.baseStats.healthModifier * 100} %<br>";
        charaDescription += $"Speed : {character.baseStats.speedModifier * 100} %<br>";
        charaDescription += $"Damages : {character.baseStats.damageModifier * 100} %<br>";
        charaDescription += $"Cooldown : {character.baseStats.cooldownModifier * 100} %<br>";
        charaDescription += $"Duration : {character.baseStats.durationModifier * 100} %<br>";
        charaDescription += $"ProjectileSpeed : {character.baseStats.projectilSpeedModifier * 100} %<br>";
        charaDescription += $"AreaSize : {character.baseStats.sizeModifier * 100} %<br>";
        charaDescription += $"Piercing : {character.baseStats.piercingBonus}<br>";
        charaDescription += $"ProjectileCount : {character.baseStats.countBonus}<br>";

        textArea.text = charaDescription;
    }

    private void OnCLicked()
    {
        if (character == null) return;

        Player.instance.ResetEntity(character);
        GameManager.StartGame();
    }
}
