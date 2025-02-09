using System;

public static class StatsHandler
{
    /// <summary>
    /// Apply all the modifiers of the character on the weapon.
    /// </summary>
    /// <param name="_weapon"></param>
    /// <param name="_holder"></param>
    public static WeaponStats ApplyWeaponModifier(this WeaponStats _weapon, EntityStats _holder) => new WeaponStats(_weapon, _holder);

    public static EntityStats ApplyStatsModifier(this EntityStats _entity, EntityStats _modifier) => new EntityStats(_entity, _modifier);
}

[Serializable]
public class EntityStats
{
    public EntityStats(EntityStats _entity, EntityStats _modifier)
    {
        healthModifier = _entity.healthModifier + _modifier.healthModifier;
        speedModifier = _entity.speedModifier + _modifier.speedModifier;

        damageModifier = _entity.damageModifier + _modifier.damageModifier;
        cooldownModifier = _entity.cooldownModifier + _modifier.cooldownModifier;
        projectilSpeedModifier = _entity.projectilSpeedModifier + _modifier.projectilSpeedModifier;
        durationModifier = _entity.durationModifier + _modifier.durationModifier;
        sizeModifier = _entity.sizeModifier + _modifier.sizeModifier;
        knockbackMultiplier = _entity.knockbackMultiplier + _modifier.knockbackMultiplier;

        piercingBonus = _entity.piercingBonus + _modifier.piercingBonus;
        countBonus = _entity.countBonus + _modifier.countBonus;
    }

    public float healthModifier;
    public float speedModifier;

    public float damageModifier;
    public float cooldownModifier;
    public float projectilSpeedModifier;
    public float durationModifier;
    public float sizeModifier;
    public float knockbackMultiplier;

    public int piercingBonus;
    public int countBonus;
}

[Serializable]
public class WeaponStats
{
    public WeaponStats(WeaponStats _weaponStats, EntityStats _entityStats)
    {
        damage = _weaponStats.damage * _entityStats.damageModifier;
        cooldown = _weaponStats.cooldown * _entityStats.cooldownModifier;
        speed = _weaponStats.speed * _entityStats.projectilSpeedModifier;
        duration = _weaponStats.duration * _entityStats.durationModifier;
        size = _weaponStats.size * _entityStats.sizeModifier;
        knockBack = _weaponStats.knockBack * _entityStats.knockbackMultiplier;
        interval = _weaponStats.interval;

        piercing = _weaponStats.piercing + _entityStats.piercingBonus;
        count = _weaponStats.count + _entityStats.countBonus;
    }

    public string levelDescription;

    public float damage;
    public float cooldown;
    public float speed;
    public float duration;
    public float size;
    public float knockBack;
    public float interval;
 
    public int piercing;
    public int count;
}
