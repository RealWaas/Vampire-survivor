using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats instance;
    [SerializeField] private TMP_Text textArea;
    private CharacterDataSO character;
    [SerializeField] private Button startButton;

    private void Awake()
    {
        instance = this;
        startButton.onClick.AddListener(OnCLicked);
    }
    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(OnCLicked);
    }

    public void SetCharacterStats(CharacterDataSO _data)
    {
        character = _data;
        string charaDescription = "";

        //Infos
        charaDescription += $"name : {character.entityName}<br>";
        charaDescription += $"weapon : {character.startingWeapon.weaponName}<br>";

        //Stats
        charaDescription += $"health : {character.healthModifier}<br>";
        charaDescription += $"speed : {character.speedModifier}<br>";
        charaDescription += $"damages : {character.damageModifier}<br>";
        charaDescription += $"cooldown : {character.cooldownModifier}<br>";
        charaDescription += $"duration : {character.durationModifier}<br>";
        charaDescription += $"projectileSpeed : {character.projectileSpeedModifier}<br>";
        charaDescription += $"areaSize : {character.areaSizeModifier}<br>";
        charaDescription += $"piercing : {character.piercingBonus}<br>";
        charaDescription += $"projectileCount : {character.projectileCountBonus}<br>";

        textArea.text = charaDescription;
    }

    private void OnCLicked()
    {
        if (character == null) return;

        Player.instance.SetStats(character);
        GameManager.SetGameState(GameState.InGame);
    }
}
