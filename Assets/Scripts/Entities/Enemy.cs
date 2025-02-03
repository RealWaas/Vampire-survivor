using UnityEngine;

[RequireComponent(typeof(MovementSystem))]
public class Enemy : Entity
{
    [SerializeField] EnemyDataSO enemyData;
    [SerializeField] MovementSystem movementSystem;
    [SerializeField] ExpCrystal crystalPrefab;

    [SerializeField] WeaponSystem weaponSystem;

    protected override void Start()
    {
        movementSystem = GetComponent<MovementSystem>();
        ResetValues(enemyData);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag)) return;

        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(damageModifier);
        }

        //entity.OnCollided();
    }

    private void Update()
    {
        Vector2 movementDir = Player.instance.transform.position - transform.position;

        if(movementDir.magnitude > 13)
        {
            SpawnManager.instance.RespawnEnemy(this);
            return;
        }

        movementSystem.MoveEntity(movementDir.normalized * speedModifier);
    }
    public override void SetStats(EntityDataSO _entityData)
    {
        base.SetStats(_entityData);

        if (_entityData.startingWeapon)
        {
            weaponSystem.InitWeapon(_entityData.startingWeapon, 0, this);
        }
    }
    protected virtual void ResetValues(EnemyDataSO _enemy)
    {
        healthSystem.ResetHealth(10 * _enemy.healthModifier);
        speedModifier = _enemy.speedModifier;
        damageModifier = _enemy.damageModifier;
    }

    protected override void HandleDeath()
    {
        ExpCrystal expCrystal = Instantiate(crystalPrefab, transform.position, Quaternion.identity);
        expCrystal.SetExpAmount(enemyData.expAmount);
        Destroy(gameObject);
    }
}