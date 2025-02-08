using UnityEngine;

public abstract class WeaponSystem : MonoBehaviour
{
    [SerializeField] public WeaponBaseDataSO weaponData;
    public Entity bearer { get; protected set; }

    public WeaponStats weaponStats;
    public int level { get; protected set; } = 0;

    protected float cooldownTimer;
    protected float nextAttackTimer = 0;

    //Attack per seconds
    //float AttackInterval => 1f / cooldownTimer;

    public void InitWeapon(WeaponBaseDataSO _weaponData, int _weaponLevel, Entity _bearer)
    {
        weaponData = _weaponData;
        bearer = _bearer;
        level = _weaponLevel;

        weaponStats = _weaponData.levelStats[level].ApplyWeaponModifier(bearer.stats);
        SetCooldown();
    }

    protected void SetCooldown()
    {
        cooldownTimer = weaponStats.cooldown;
        nextAttackTimer = Time.time + cooldownTimer;
    }

    protected virtual void Update()
    {
        if (Time.time >= nextAttackTimer && nextAttackTimer != 0)
        {
            PerformAttack();
            nextAttackTimer = Time.time + cooldownTimer;
            
            //Attack per seconds
            //nextAttackTimer = Time.time + AttackInterval;
        }
    }

    public void LevelUpWeapon() => level++;

    /// <summary>
    /// Instantiate the attack and initialize its controls.
    /// </summary>
    /// <param name="_attacker"></param>
    protected abstract void PerformAttack();
}
