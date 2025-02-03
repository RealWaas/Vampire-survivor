using UnityEngine;

public class RangedSystem : WeaponSystem
{
    protected override void PerformAttack(Entity _bearer)
    {
        Entity targetEntity = EntityTracker.GetClosestEntity(transform.position, 10, _bearer.tag);

        GameObject attack = Instantiate(weaponData.attackPrefab, transform.position, Quaternion.identity);

        if (targetEntity)
            attack.transform.right = attack.transform.position - targetEntity.transform.position;

        //attack.transform.localPosition = attackOffset;

        // Geting its controller component
        if (attack.TryGetComponent(out AttackController attackController))
            attackController.InitializeAttack(this);
    }
}
