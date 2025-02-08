using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ItemSelection;
    [SerializeField] private GameObject CharacterSelection;
    [SerializeField] private GameObject MainMenu;

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
        GameManager.OnInGameState -= InGameState;
    }

    private void Start()
    {
        OpenMenu();
    }


    /// <summary>
    /// Hide all menues
    /// </summary>
    private void HideAll()
    {
        MainMenu.SetActive(false);
        CharacterSelection.SetActive(false);
        ItemSelection.SetActive(false);
    }

    private void OpenMenu()
    {
        HideAll();
        MainMenu.SetActive(true);
    }

    private void OpenCharacterSelection()
    {
        HideAll();
        CharacterSelection.SetActive(true);
    }

    private void OpenItemSelection()
    {
        HideAll();
        ItemSelection.SetActive(true);
    }

    private void InGameState()
    {
        HideAll();
    }
}
