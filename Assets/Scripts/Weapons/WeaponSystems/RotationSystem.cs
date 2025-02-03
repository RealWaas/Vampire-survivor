using UnityEngine;

public class RotationSystem : WeaponSystem
{
    protected override void PerformAttack(Entity _bearer)
    {
        GameObject attack = Instantiate(weaponData.attackPrefab, transform);

        // Geting its controller component
        if (attack.TryGetComponent(out AttackController attackController))
            attackController.InitializeAttack(this);
    }
    protected override void Update()
    {
        if (Time.time >= nextAttackTimer && nextAttackTimer != 0)
        {
            PerformAttack(bearer);
            nextAttackTimer = Time.time + cooldownTimer + weaponData.levelStats[level].baseDuration * bearer.durationModifier;

            //Attack per seconds
            //nextAttackTimer = Time.time + AttackInterval;
        }
    }
}
