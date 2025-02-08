using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class Vases : MonoBehaviour
{
    [SerializeField] List<GameObject> dropList = new List<GameObject>();
    [SerializeField] HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem.OnHealthEmpty += HandleDeath;
    }

    private void OnEnable()
    {
        healthSystem.ResetHealth(1);
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthEmpty -= HandleDeath;
    }

    private void HandleDeath()
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

        Destroy(gameObject);
    }
}
