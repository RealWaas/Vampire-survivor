using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(MovementSystem))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected HealthSystem healthSystem;
    protected MovementSystem movementHandler;
    public GameObject weaponHolder;

    protected const float BASE_HEALTH = 100;
    protected const float BASE_SPEED = 1;
    protected const float BASE_DAMAGE = 1;

    public EntityStats stats { get; protected set; }

    protected virtual void Awake()
    {
        healthSystem.OnHealthEmpty += HandleDeath;
        healthSystem.OnTakeDamage += TakeDamage;
        
        movementHandler = GetComponent<MovementSystem>();
    }
    private void OnDestroy()
    {
        healthSystem.OnHealthEmpty -= HandleDeath;
        healthSystem.OnTakeDamage -= TakeDamage;
    }

    /// <summary>
    /// Reset the entity to respawn it.
    /// </summary>
    /// <param name="_entityData"></param>
    public abstract void ResetEntity(EntityDataSO _entityData);

    protected abstract void TakeDamage(float _knockBack);
    protected abstract void HandleDeath();
}