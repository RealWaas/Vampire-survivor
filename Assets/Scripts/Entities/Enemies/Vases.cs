using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class Vases : BaseEnemy
{
    [SerializeField] List<GameObject> dropList = new List<GameObject>();

    protected override void Move(Vector2 _playerDir)
    {
        // Kept empty to avoid vases to respawn when the player is too far from it
    }
    protected override void TakeDamage(float _knockBack)
    {
        // Kept empty to avoid knockback
    }

    protected override void DropItemOnDeath()
    {

        GameObject randomPrefab = dropList[Random.Range(0, dropList.Count)];

        GameObject item = PoolManager.GetAvailableObjectFromPool(randomPrefab);

        if (!item)
        {
            item = Instantiate(randomPrefab, transform.position, Quaternion.identity);
            PoolManager.CreateObject(randomPrefab, item);
        }
        else
        {
            item.SetActive(true);
            item.transform.position = transform.position;
        }
    }
}
