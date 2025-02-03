using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ItemSelection;
    [SerializeField] private GameObject CharacterSelection;

    private void Awake()
    {
        GameManager.OnInMenuState += OpenMenu;
        GameManager.OnCharacterSelection += OpenCharacterSelection;
        GameManager.OnItemSelection += OpenItemSelection;
        GameManager.OnInGameState += InGameState;
    }

    private void OnDestroy()
    {
        GameManager.OnInMenuState -= OpenMenu;
        GameManager.OnCharacterSelection -= OpenCharacterSelection;
        GameManager.OnItemSelection -= OpenItemSelection;
        GameManager.OnInGameState -= InGameState;
    }

    private void Start()
    {
        CharacterSelection.SetActive(true);

        ItemSelection.SetActive(false);
    }

    private void OpenMenu()
    {
        ItemSelection.SetActive(false);
    }

    private void OpenCharacterSelection()
    {
        CharacterSelection.SetActive(true);

        ItemSelection.SetActive(false);
    }

    private void OpenItemSelection()
    {
        ItemSelection.SetActive(true);

        CharacterSelection.SetActive(false);
    }

    private void InGameState()
    {
        CharacterSelection.SetActive(false);
        ItemSelection.SetActive(false);
    }

}
