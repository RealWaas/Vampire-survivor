using UnityEngine;

[RequireComponent(typeof(MovementHandler))]
[RequireComponent(typeof(HealthSystem))]
public abstract class Entity : MonoBehaviour
{
    protected MovementHandler movementHandler;

    protected HealthSystem healthSystem;

    public float moveSpeed { get; protected set; }
    public int damage { get; protected set; }

    protected virtual void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        movementHandler = GetComponent<MovementHandler>();

        healthSystem.OnHealthEmpty += HandleDeath;
    }
    private void OnDestroy()
    {
        healthSystem.OnHealthEmpty -= HandleDeath;
    }

    protected abstract void HandleDeath();
}
