using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            Debug.Log(collectable.ToString());
            collectable.OnMagnet();
        }
    }
}
