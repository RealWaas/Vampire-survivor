using UnityEngine;

public abstract class EntityDataSO : ScriptableObject
{
    public string entityName;

    public EntityStats baseStats;

    public ActiveItemSO startingWeapon;
}
