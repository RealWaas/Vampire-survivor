using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out ICollectable collectable))
            collectable.OnCollect();
    }
}
