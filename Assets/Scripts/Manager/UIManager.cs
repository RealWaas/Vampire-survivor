using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ItemSelection;

    private void Awake()
    {
        GameManager.OnInMenuState += OpenMenu;
        GameManager.OnItemSelection += OpenItemSelection;
        GameManager.OnInGameState += InGameState;
    }

    private void OnDestroy()
    {
        GameManager.OnInMenuState -= OpenMenu;
        GameManager.OnItemSelection -= OpenItemSelection;
        GameManager.OnInGameState -= InGameState;
    }

    private void Start()
    {
        ItemSelection.SetActive(false);
    }

    private void OpenMenu()
    {
        ItemSelection.SetActive(false);
    }

    private void OpenItemSelection()
    {
        ItemSelection.SetActive(true);
    }

    private void InGameState()
    {
        ItemSelection.SetActive(false);
    }

}
