using UnityEngine;

public class ProjectileController : AttackController
{
    protected int piercing;
    protected float speed;

    protected override void Update()
    {
        base.Update();
        // Move projectile forward
        Debug.Log(speed);
        transform.position += -transform.right * (speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // The attack ignore the attacker
        if (collision.CompareTag(attacker.gameObject.tag)) return;

        if (collision.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(damage);
            piercing--;
        }
        // Allow the projectile to go through multiple enemies before breaking
        if(piercing <= 0)
            Destroy(gameObject);
    }

    public override void InitializeAttack(Entity _attacker, AttackBehaviourSO _attack)
    {
        base.InitializeAttack(_attacker, _attack);
        piercing = _attack.basePiercing + _attacker.piercingBonus;
        speed = _attack.baseProjectileSpeed * _attacker.projectileSpeedModifier;
        //Debug.Log(_attack.baseProjectileSpeed + " * " + _attacker.projectileSpeedModifier);
    }
}
