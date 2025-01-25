using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButton : Button, IPointerDownHandler
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        GameManager.SetGameState(GameState.InGame);
        this.gameObject.SetActive(false);
    }
}
