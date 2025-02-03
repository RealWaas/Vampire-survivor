using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPlaceholder : MonoBehaviour
{
    Button mainButton;
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text weaponDescription;
    [SerializeField] Image weaponSprite;
    [SerializeField] WeaponDataSO weapon;

    public void Awake()
    {
        mainButton = GetComponent<Button>();
        mainButton.onClick.AddListener(OnCLicked);
    }

    public void SetWeapon(WeaponDataSO _weapon, int _currentLevel)
    {
        weapon = _weapon;
        weaponName.text = weapon.weaponName;
        weaponDescription.text = weapon.levelStats[_currentLevel].levelDescription;
        weaponSprite.sprite = weapon.weaponSprite;
    }

    public void OnDestroy()
    {
        mainButton.onClick.RemoveListener(OnCLicked);
    }

    private void OnCLicked()
    {
        if (weapon == null) return;

        AttackManager.UpdateWeapon(weapon);
        GameManager.SetGameState(GameState.InGame);
    }
}
