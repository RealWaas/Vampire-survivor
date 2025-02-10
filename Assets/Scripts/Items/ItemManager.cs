using System.Collections.Generic;

public static class ItemManager
{
    public const int MAX_ACTIVE_COUNT = 5;
    public const int MAX_PASSIVE_COUNT = 5;

    public static Dictionary<ActiveItemSO, WeaponSystem> actifList = new Dictionary<ActiveItemSO, WeaponSystem>();
    public static Dictionary<PassifItemSO, int> passifList = new Dictionary<PassifItemSO, int>();

    /// <summary>
    /// LevelUp weapons or add one to the player.
    /// </summary>
    /// <param name="_item"></param>
    public static void UpdateItem(ItemSO _item)
    {
        if(_item is ActiveItemSO)
        {
            UpdateActif((ActiveItemSO)_item);
        }
        else if (_item is PassifItemSO)
        {
            UpdatePassif((PassifItemSO)_item);
        }
    }

    private static void UpdateActif(ActiveItemSO _actif)
    {
        if (actifList.ContainsKey(_actif)) // Level up weapon
            actifList[_actif].LevelUpWeapon();

        else //Add weapon to inventory
        {
            WeaponSystem weaponSystem = _actif.InitWeapon(Player.instance);
            actifList.Add(_actif, weaponSystem);
        }
    }

    private static void UpdatePassif(PassifItemSO _passif)
    {
        if (passifList.ContainsKey(_passif)) // Level up item (its shouldn't appears if not available)
            passifList[_passif] ++;

        else //Add item to inventory
            passifList.Add(_passif, 0);

        Player.instance.UpdateStats(_passif.levelStats);

        foreach(WeaponSystem system in actifList.Values)
        {
            system.UpdateStats();
        }
    }

    public static void ResetWeapons()
    {
        foreach(WeaponSystem weapon in actifList.Values)
            weapon.RemoveWeapon();

        actifList = new Dictionary<ActiveItemSO, WeaponSystem>();
    }
}