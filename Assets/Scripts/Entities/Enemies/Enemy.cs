using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] EnemyData enemyData;

    protected override void Start()
    {
        base.Start();

        ResetValues(enemyData);
    }

    protected virtual void ResetValues(EnemyData _enemy)
    {
        healthSystem.ResetHealth(_enemy.maxHealth);
        moveSpeed = _enemy.moveSpeed;
        damage = _enemy.damage;
    }

    protected virtual void FixedUpdate()
    {
        Vector3 direction = Player.Instance.transform.position - transform.position;
        movementHandler.MoveEntity(direction.normalized * moveSpeed);
    }

    protected override void HandleDeath()
    {
        Destroy(gameObject);
    }
}
