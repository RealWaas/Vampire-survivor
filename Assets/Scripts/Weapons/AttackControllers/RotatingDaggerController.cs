using UnityEngine;

public class RotatingDaggerController : AttackController
{
    protected override void Update()
    {
        base.Update();
        // Move projectile forward
        transform.position += -transform.right * (weaponStats.speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // The attack ignore the attacker
        if (collision.gameObject.CompareTag(attackerTag)) return;

        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(weaponStats.damage);
            weaponStats.piercing--;
        }
        // Allow the projectile to go through multiple enemies before breaking
        if(weaponStats.piercing < 0)
            gameObject.SetActive(false);
    }

    public override void InitializeAttack(WeaponSystem _weapon)
    {
        base.InitializeAttack(_weapon);
    }
}
