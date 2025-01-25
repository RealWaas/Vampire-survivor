using UnityEngine;

public abstract class AttackController : MonoBehaviour
{
    protected Entity attacker;

    protected int damage;
    protected float speed;
    protected float duration;
    protected float areaSize;

    protected int piercing;

    protected float despawnTime;

    protected virtual void Update()
    {
        if (Time.time >= despawnTime)
        {
            Destroy(gameObject);
        }

        // Move projectile
        //transform.position += transform.forward * (projectileSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // The attack ignore the attacker
        if (collision.CompareTag(attacker.tag)) return;
    }

    public virtual void InitializeAttack(Entity _attacker, AttackBehaviourSO _attack)
    {
        damage = _attack.damage;
        speed = _attack.speed;
        duration = _attack.duration;
        areaSize = _attack.areaSize;
        piercing = _attack.piercing;

        despawnTime = Time.time + duration;
    }
}
