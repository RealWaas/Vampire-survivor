using UnityEngine;

public class MeleeSystem : WeaponSystem
{
    protected override void PerformAttack(Entity _bearer)
    {
        GameObject attack = Instantiate(weaponData.attackPrefab, transform.position, Quaternion.identity);
        attack.transform.localPosition += weaponData.attackOffset;

        // Geting its controller component
        if (attack.TryGetComponent(out AttackController attackController))
            attackController.InitializeAttack(this);
    }
}
