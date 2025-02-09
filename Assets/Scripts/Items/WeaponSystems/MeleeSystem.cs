using UnityEngine;

public class MeleeSystem : WeaponSystem
{
    protected override void PerformAttack()
    {
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

        attack.transform.localPosition += weaponData.attackOffset;

        // Geting its controller component
        if (attack.TryGetComponent(out AttackController attackController))
            attackController.InitializeAttack(this);
    }
}
