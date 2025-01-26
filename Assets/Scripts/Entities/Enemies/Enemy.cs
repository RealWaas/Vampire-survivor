using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(MovementSystem))]
public class Enemy : Entity
{
    [SerializeField] EnemyData enemyData;
    MovementSystem movementSystem;

    protected override void Start()
    {
        base.Start();

        movementSystem = GetComponent<MovementSystem>();
        ResetValues(enemyData);

        InitMovements();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(10);
        }

        //entity.OnCollided();
    }

    private void InitMovements()
    {
        Player targetPlayer = GameManager.playerList
            .OrderBy(player =>
            {
                return Vector3.Distance(player.transform.position, transform.position);
            })
            .FirstOrDefault();

        movementSystem.InitSystem(targetPlayer.transform, 1);
    }
    protected virtual void ResetValues(EnemyData _enemy)
    {
        healthSystem.ResetHealth(_enemy.maxHealth);
        speedModifier = _enemy.moveSpeed;
        damagerModifier = _enemy.damage;
    }

    protected override void HandleDeath()
    {
        Destroy(gameObject);
    }
}