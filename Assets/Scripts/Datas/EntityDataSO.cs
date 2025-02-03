using UnityEngine;

public abstract class EntityDataSO : ScriptableObject
{
    public string entityName;
    public float healthModifier;
    public float speedModifier;

    public float damageModifier;
    public float cooldownModifier;
    public float durationModifier;
    public float projectileSpeedModifier;
    public float areaSizeModifier;

    public int piercingBonus;
    public int projectileCountBonus;

    public WeaponDataSO startingWeapon;
}
