using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RestartButton : Button, IPointerDownHandler
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        GameManager.ResetGame();
    }
}
