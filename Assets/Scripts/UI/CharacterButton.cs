using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    Button mainButton;
    [SerializeField] TMP_Text characterName;
    [SerializeField] Image weaponSprite;
    [SerializeField] EntityDataSO character;

    public void Awake()
    {
        mainButton = GetComponent<Button>();
        mainButton.onClick.AddListener(OnCLicked);
    }

    public void SetCharacter(CharacterDataSO _character)
    {
        character = _character;
        //characterName.text = character.weaponName;
        //weaponSprite.sprite = character.weaponSprite;
    }

    public void OnDestroy()
    {
        mainButton.onClick.RemoveListener(OnCLicked);
    }

    private void OnCLicked()
    {
        if (character == null) return;

        Player.instance.SetStats(character);
        GameManager.SetGameState(GameState.InGame);
    }
}