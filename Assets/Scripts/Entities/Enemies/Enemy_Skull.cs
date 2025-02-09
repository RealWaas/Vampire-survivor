using UnityEngine;

public class Enemy_Skull : BaseEnemy
{
    protected override void Move(Vector2 _playerDir)
    {
        base.Move(_playerDir);

        // Inversed, the skull sprite is looking toward the left
        enemyRenderer.flipX = _playerDir.x > 0;

        movementHandler.MoveEntity(_playerDir.normalized * BASE_SPEED * stats.speedModifier);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag)) return;

        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(BASE_DAMAGE * stats.damageModifier);
        }
    }
}
