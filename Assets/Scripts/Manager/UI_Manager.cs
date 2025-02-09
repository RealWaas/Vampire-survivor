using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject ItemSelection;
    [SerializeField] private GameObject CharacterSelection;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject GameOver;

    private void Awake()
    {
        GameManager.OnInMenuState += OpenMenu;
        GameManager.OnCharacterSelection += OpenCharacterSelection;
        GameManager.OnItemSelection += OpenItemSelection;
        GameManager.OnInGameState += InGameState;
        GameManager.OnGameOverState += OpenGameOver;
    }

    private void OnDestroy()
    {
        GameManager.OnInMenuState -= OpenMenu;
        GameManager.OnCharacterSelection -= OpenCharacterSelection;
        GameManager.OnInGameState -= InGameState;
        GameManager.OnGameOverState -= OpenGameOver;
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
        GameOver.SetActive(false);
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

    private void OpenGameOver()
    {
        HideAll();
        GameOver.SetActive(true);
    }

    private void InGameState()
    {
        HideAll();
    }
}
