using UnityEngine;

public class CollisionsHandler : MonoBehaviour
{
    [SerializeField] private Entity entity;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            if (damagable.GetType() == entity.GetType())
                return;

            damagable.TakeDamage(entity.damage);
        }

        //entity.OnCollided();
    }
}
