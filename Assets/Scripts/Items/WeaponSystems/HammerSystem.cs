using System.Collections.Generic;
using UnityEngine;

public class HammerSystem : WeaponSystem
{
    protected override void PerformAttack()
    {
        List<Entity> targets = EntityTracker.GetRandomEntity(transform.position, 10, bearer.tag, weaponStats.count);

        foreach (Entity target in targets)
        {

        }
    }
}
