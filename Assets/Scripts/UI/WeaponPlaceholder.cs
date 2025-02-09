using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPlaceholder : MonoBehaviour
{
    Button mainButton;
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text weaponDescription;
    [SerializeField] Image weaponSprite;
    [SerializeField] ItemSO item;

    public void Awake()
    {
        mainButton = GetComponent<Button>();
        mainButton.onClick.AddListener(OnCLicked);
    }


    public void SetActif(ActiveItemSO _item, int _currentLevel)
    {
        this.item = _item;
        weaponName.text = this.item.itemName;
        weaponDescription.text = _item.levelStats[_currentLevel].levelDescription;
        weaponSprite.sprite = this.item.itemSprite;
    }

    public void SetPassif(PassifItemSO _item)
    {
        item = _item;
        weaponName.text = item.itemName;
        weaponDescription.text = _item.description;
        weaponSprite.sprite = item.itemSprite;
    }

    public void OnDestroy()
    {
        mainButton.onClick.RemoveListener(OnCLicked);
    }

    private void OnCLicked()
    {
        if (item == null) return;

        ItemManager.UpdateItem(item);
        GameManager.SetGameState(GameState.InGame);
    }
}
