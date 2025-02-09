using System;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    public static event Action OnGlobalMagnet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            collectable.OnMagnet();
        }
    }

    public static void AttractAllCrystals()
    {
        OnGlobalMagnet?.Invoke();
    }
}
