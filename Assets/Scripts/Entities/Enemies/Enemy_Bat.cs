using UnityEngine;

public class Enemy_Bat : BaseEnemy
{
    private Vector2 targetDirection;
    protected override void Move(Vector2 _playerDir)
    {
        base.Move(_playerDir);

        enemyRenderer.flipX = targetDirection.x < 0;
        movementHandler.MoveEntity(targetDirection.normalized * BASE_SPEED * stats.speedModifier);
    }
    protected override void ResetMovement()
    {
        targetDirection = Player.instance.transform.position - transform.position;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag)) return;

        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(BASE_DAMAGE * stats.damageModifier);
        }
    }
}
