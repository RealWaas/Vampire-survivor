using System.Collections.Generic;

public static class AttackManager
{
    public const int MAX_WEAPON_COUNT = 5;

    public static Dictionary<WeaponDataSO, WeaponSystem> weaponList = new Dictionary<WeaponDataSO, WeaponSystem>();
    public static void UpdateWeapon(WeaponDataSO _weapon)
    {
        if(weaponList.ContainsKey(_weapon)) // Level up weapon
            weaponList[_weapon].LevelUpWeapon();

        else //Add weapon to inventory
        {
            WeaponSystem weaponSystem = _weapon.InitWeapon(Player.instance);
            weaponList.Add(_weapon, weaponSystem);
        }
    }
}