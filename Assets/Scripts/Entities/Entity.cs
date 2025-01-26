using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(HealthSystem))]
public abstract class Entity : MonoBehaviour
{
    protected HealthSystem healthSystem;

    public float healthModifier /*{ get; protected set; }*/ = 1f;
    public float speedModifier /*{ get; protected set; }*/ = 1f;


    public float damagerModifier /*{ get; protected set; }*/ = 1f;
    public float cooldownModifier /*{ get; protected set; }*/ = 1f;
    public float durationModifier /*{ get; protected set; }*/ = 1f;
    public float projectileSpeedModifier /*{ get; protected set; }*/ = 1f;
    public float areaSizeModifier /*{ get; protected set; }*/ = 1f;

    public int piercingBonus /*{ get; protected set; }*/ = 0;
    public int projectileBonus /*{ get; protected set; }*/ = 0;

    protected virtual void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnHealthEmpty += HandleDeath;
    }
    private void OnDestroy()
    {
        healthSystem.OnHealthEmpty -= HandleDeath;
    }

    protected abstract void HandleDeath();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(this.tag)) return;
    }
}
