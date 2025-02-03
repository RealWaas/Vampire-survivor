using UnityEngine;

public abstract class AttackController : MonoBehaviour
{
    protected string attackerTag;

    protected float damage;
    protected float duration;
    protected float areaSize;

    //protected int critChance;

    protected float durationTime;

    protected virtual void Update()
    {
        // Destroy the projectile after its duration
        if (Time.time >= durationTime)
        {
            Destroy(gameObject);
        }
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);
    
    public virtual void InitializeAttack(WeaponSystem _weapon)
    {
        attackerTag = _weapon.bearer.tag;
     
        damage = _weapon.weaponData.levelStats[_weapon.level].baseDamage * _weapon.bearer.damageModifier;
        duration = _weapon.weaponData.levelStats[_weapon.level].baseDuration * _weapon.bearer.durationModifier;
        areaSize = _weapon.weaponData.levelStats[_weapon.level].baseAreaSize * _weapon.bearer.areaSizeModifier;

        durationTime = Time.time + duration;

        this.transform.localScale = this.transform.localScale * areaSize;
    }
}
