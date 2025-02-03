using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public abstract class Entity : MonoBehaviour
{
    protected HealthSystem healthSystem;

    public float healthModifier { get; protected set; } = 1f;
    public float speedModifier { get; protected set; } = 1f;


    public float damageModifier { get; protected set; } = 1f;
    public float cooldownModifier { get; protected set; } = 1f;
    public float durationModifier { get; protected set; } = 1f;
    public float projectileSpeedModifier { get; protected set; } = 1f;
    public float areaSizeModifier { get; protected set; } = 1f;

    public int piercingBonus { get; protected set; } = 0;
    public int countBonus { get; protected set; } = 0;

    protected virtual void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnHealthEmpty += HandleDeath;
    }
    private void OnDestroy()
    {
        healthSystem.OnHealthEmpty -= HandleDeath;
    }

    /// <summary>
    /// Call to set the stats of the entity.
    /// </summary>
    /// <param name="_entityData"></param>
    public virtual void SetStats(EntityDataSO _entityData)
    {
        healthModifier = _entityData.healthModifier;
        speedModifier = _entityData.speedModifier;

        damageModifier = _entityData.damagerModifier;
        cooldownModifier = _entityData.cooldownModifier;
        durationModifier = _entityData.durationModifier;
        projectileSpeedModifier = _entityData.projectileSpeedModifier;
        areaSizeModifier = _entityData.areaSizeModifier;

        piercingBonus = _entityData.piercingBonus;
        countBonus = _entityData.projectileCountBonus;
    }

    protected abstract void HandleDeath();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
