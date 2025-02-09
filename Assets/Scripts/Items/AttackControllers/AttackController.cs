using UnityEngine;

public abstract class AttackController : MonoBehaviour
{
    protected string attackerTag;

    protected float durationTime;

    protected WeaponStats weaponStats;

    protected virtual void Update()
    {
        // Disable the projectile after its duration
        if (Time.time >= durationTime)
        {
            gameObject.SetActive(false);
        }
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);
    
    public virtual void InitializeAttack(WeaponSystem _weapon)
    {
        attackerTag = _weapon.bearer.tag;

        weaponStats = _weapon.weaponData.levelStats[_weapon.level].ApplyWeaponModifier(_weapon.bearer.stats);

        durationTime = Time.time + weaponStats.duration;

        this.transform.localScale = Vector3.one;
        this.transform.localScale = this.transform.localScale * weaponStats.size;
    }
}
