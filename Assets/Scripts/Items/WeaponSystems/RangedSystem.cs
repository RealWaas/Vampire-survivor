using System.Collections;
using UnityEngine;

public class RangedSystem : WeaponSystem
{
    protected override void PerformAttack()
    {
        StartCoroutine(FireProjectiles());
    }

    private IEnumerator FireProjectiles()
    {
        for(int index = 0; index < weaponStats.count; index++)
        {
            Entity targetEntity = EntityTracker.GetClosestEntity(transform.position, 10, bearer.tag);

            GameObject attack = PoolManager.GetAvailableObjectFromPool(weaponData.attackPrefab);

            if (!attack)
            {
                attack = Instantiate(weaponData.attackPrefab, transform.position, Quaternion.identity);
                PoolManager.CreateObject(weaponData.attackPrefab, attack);
            }
            else
            {
                attack.SetActive(true);
                attack.transform.position = transform.position;
            }

            if (targetEntity)
                attack.transform.right = attack.transform.position - targetEntity.transform.position;

            //attack.transform.localPosition = attackOffset;

            // Geting its controller component
            if (attack.TryGetComponent(out AttackController attackController))
                attackController.InitializeAttack(this);

            yield return new WaitForSeconds(weaponStats.interval);
        }
    }
}
