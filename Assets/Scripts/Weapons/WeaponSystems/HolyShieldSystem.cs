using System.Collections.Generic;
using UnityEngine;

public class HolyShieldSystem : WeaponSystem
{
    protected override void PerformAttack()
    {
        List<Entity> targets = EntityTracker.GetRandomEntity(transform.position, 10, bearer.tag, weaponStats.count);


        foreach (Entity target in targets)
        {

            Debug.Log(target.name);
            GameObject attack = PoolManager.GetAvailableObjectFromPool(weaponData.attackPrefab);

            if (!attack)
            {
                attack = Instantiate(weaponData.attackPrefab, target.transform.position, Quaternion.identity);
                PoolManager.CreateObject(weaponData.attackPrefab, attack);
            }
            else
            {
                attack.SetActive(true);
                attack.transform.position = target.transform.position;
            }

            //attack.transform.localPosition += weaponData.attackOffset;

            // Geting its controller component
            if (attack.TryGetComponent(out AttackController attackController))
                attackController.InitializeAttack(this);
        }
    }
}
