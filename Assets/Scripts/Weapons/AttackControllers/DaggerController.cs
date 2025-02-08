using UnityEngine;

public class DaggerController : AttackController
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // The attack ignore the attacker
        if (collision.CompareTag(attackerTag)) return;

        if (collision.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(weaponStats.damage);
        }
    }
}
