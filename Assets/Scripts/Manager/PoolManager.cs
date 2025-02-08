using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PoolManager
{
    public static Dictionary<GameObject, List<GameObject>> objectPools =
        new Dictionary<GameObject, List<GameObject>>();

    /// <summary>
    /// Return the first disabled object from the correct pool.
    /// <br></br>
    /// Handle the creation of a pool if the key is not found.
    /// </summary>
    /// <param name="_key"></param>
    /// <returns></returns>
    public static GameObject GetAvailableObjectFromPool(GameObject _key)
    {
        // Create a pool of the given key if not existing
        if(!objectPools.ContainsKey(_key))
        {
            CreateNewPool(_key);
            return null;
        }
        
        // Return null if the pool is empty
        if(objectPools[_key].Count == 0)
            return null;

        // Find the first element inactive
        GameObject availableEnemy = objectPools[_key].Where(element =>
        {
            return element.gameObject.activeSelf == false;
        }).FirstOrDefault();

        return availableEnemy;
    }

    /// <summary>
    /// Create a new pool with a prefab as key.
    /// </summary>
    /// <param name="_key"></param>
    private static void CreateNewPool(GameObject _key) => objectPools.Add(_key, new List<GameObject>());

    /// <summary>
    /// Add an object to its pool.
    /// </summary>
    /// <param name="_key"></param>
    /// <param name="_value"></param>
    public static void CreateObject(GameObject _key, GameObject _value) => objectPools[_key].Add(_value);
}
