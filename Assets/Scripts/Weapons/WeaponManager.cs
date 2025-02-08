using System.Collections.Generic;

public static class WeaponManager
{
    public const int MAX_WEAPON_COUNT = 5;

    public static Dictionary<WeaponBaseDataSO, WeaponSystem> weaponList = new Dictionary<WeaponBaseDataSO, WeaponSystem>();

    /// <summary>
    /// LevelUp weapons or add one to the player.
    /// </summary>
    /// <param name="_weapon"></param>
    public static void UpdateWeapon(WeaponBaseDataSO _weapon)
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