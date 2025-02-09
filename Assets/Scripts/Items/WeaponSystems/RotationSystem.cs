using UnityEngine;

public class RotationSystem : WeaponSystem
{
    protected override void PerformAttack()
    {
        GameObject attack = PoolManager.GetAvailableObjectFromPool(weaponData.attackPrefab);

        if (!attack)
        {
            attack = Instantiate(weaponData.attackPrefab, transform);
            PoolManager.CreateObject(weaponData.attackPrefab, attack);
        }
        else
        {
            attack.SetActive(true);

        }

        // Geting its controller component
        if (attack.TryGetComponent(out AttackController attackController))
            attackController.InitializeAttack(this);
    }
    protected override void Update()
    {
        if (Time.time >= nextAttackTimer && nextAttackTimer != 0)
        {
            PerformAttack();
            nextAttackTimer = Time.time + cooldownTimer + weaponStats.duration;

            //Attack per seconds
            //nextAttackTimer = Time.time + AttackInterval;
        }
    }
}
