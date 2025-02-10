using System.Collections.Generic;
using UnityEngine;

public class MeleeController : AttackController
{
    protected float nextAttackTimer = 0;
    protected float cooldownTimer;

    private List<HealthSystem> healthSystemList = new List<HealthSystem>();

    public override void InitializeAttack(WeaponSystem _weapon)
    {
        base.InitializeAttack(_weapon);
        cooldownTimer = weaponStats.interval;
        nextAttackTimer = Time.time + cooldownTimer;
    }

    protected override void Update()
    {
        base.Update();

        //if (Time.time >= nextAttackTimer && nextAttackTimer != 0)
        //{
        //    foreach(HealthSystem healthSystem in healthSystemList)
        //    {
        //        healthSystem.TakeDamage(weaponStats.damage);
        //    }

        //    nextAttackTimer = Time.time + cooldownTimer;
        //}
    }



    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(attackerTag)) return;

        if (other.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(weaponStats.damage);

            // Save entity to do damages later
            healthSystemList.Add(healthSystem);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove entity when they get out of the collider
        if (other.CompareTag(attackerTag)) return;

        if (other.gameObject.TryGetComponent(out HealthSystem entity))
        {
            healthSystemList.Remove(entity);
        }
    }
}
