using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LaunchButton : Button, IPointerDownHandler
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        GameManager.SetGameState(GameState.CharacterSelection);
    }
}
