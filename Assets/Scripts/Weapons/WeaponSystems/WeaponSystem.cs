using UnityEngine;

public abstract class WeaponSystem : MonoBehaviour
{
    [SerializeField] public WeaponDataSO weaponData;
    public Entity bearer { get; protected set; }
    public int level { get; protected set; } = 0;

    protected float cooldownTimer;
    protected float nextAttackTimer = 0;

    //Attack per seconds
    //float AttackInterval => 1f / cooldownTimer;

    public void InitWeapon(WeaponDataSO _weaponData, int _weaponLevel, Entity _bearer)
    {
        weaponData = _weaponData;
        bearer = _bearer;
        level = _weaponLevel;
        SetCooldown();
    }

    protected void SetCooldown()
    {
        cooldownTimer = weaponData.levelStats[level].baseCooldown * bearer.cooldownModifier;
        nextAttackTimer = Time.time + cooldownTimer;
    }

    protected virtual void Update()
    {
        if (Time.time >= nextAttackTimer && nextAttackTimer != 0)
        {
            PerformAttack(bearer);
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
    protected abstract void PerformAttack(Entity _bearer);
}
