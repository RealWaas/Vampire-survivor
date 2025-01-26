using UnityEngine;

public abstract class AttackController : MonoBehaviour
{
    protected Entity attacker;

    protected float damage;
    protected float duration;
    protected float areaSize;

    //protected int critChance;

    protected float durationTime;

    protected virtual void Update()
    {
        // Destroy the projectile after its duration
        if (Time.time >= durationTime)
        {
            Destroy(gameObject);
        }
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);
    
    public virtual void InitializeAttack(Entity _attacker, AttackBehaviourSO _attack)
    {
        attacker = _attacker;
     
        damage = _attack.baseDamage * _attacker.damagerModifier;
        duration = _attack.baseDuration * _attacker.durationModifier;
        areaSize = _attack.baseAreaSize * _attacker.areaSizeModifier;

        durationTime = Time.time + duration;

        this.transform.localScale = this.transform.localScale * areaSize;
    }
}
