using UnityEngine;

public class ProjectileController : AttackController
{
    protected override void Update()
    {
        base.Update();
        // Move projectile forward
        transform.position += -transform.right * (speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public override void InitializeAttack(Entity _attacker, AttackBehaviourSO _attack)
    {
        base.InitializeAttack(attacker, _attack);
    }
}
