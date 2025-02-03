using UnityEngine;

public class ProjectileController : AttackController
{
    protected int piercing;
    protected float speed;

    protected override void Update()
    {
        base.Update();
        // Move projectile forward
        transform.position += -transform.right * (speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // The attack ignore the attacker
        if (collision.gameObject.CompareTag(attackerTag)) return;


        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            Debug.Log(collision.gameObject);
            healthSystem.TakeDamage(damage);
            piercing--;
        }
        // Allow the projectile to go through multiple enemies before breaking
        if(piercing <= 0)
            Destroy(gameObject);
    }

    public override void InitializeAttack(WeaponSystem _weapon)
    {
        base.InitializeAttack(_weapon);
        piercing = _weapon.weaponData.levelStats[_weapon.level].basePiercing + _weapon.bearer.piercingBonus;
        speed = _weapon.weaponData.levelStats[_weapon.level].baseProjectileSpeed * _weapon.bearer.projectileSpeedModifier;
    }
}
