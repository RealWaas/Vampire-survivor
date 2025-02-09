using System.Collections;
using UnityEngine;

public class Enemy_Death : BaseEnemy
{
    [SerializeField] private float maxPlayerDist = 5f;

    private bool isInRage = false;
    private bool canEnterRage = true;

    private Vector2 targetDirection;

    private float currentSpeed;

    public override void ResetEntity(EntityDataSO _entityData)
    {
        stats = _entityData.baseStats;
        healthSystem.ResetHealth(BASE_HEALTH * stats.healthModifier);
        ResetMovement();
        currentSpeed = stats.speedModifier;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    protected override void Move(Vector2 _playerDir)
    {
        base.Move(_playerDir);

        
        // Starting rage make it go forward of its current direction
        if (_playerDir.magnitude < maxPlayerDist && canEnterRage)
        {
            StartCoroutine(RageCoroutine());
        }

        else if(!isInRage) // Outside of rage, it follows the player
            targetDirection = Player.instance.transform.position - transform.position;

        enemyRenderer.flipX = targetDirection.x < 0;
        movementHandler.MoveEntity(targetDirection.normalized * BASE_SPEED * currentSpeed);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // Death can attack all entities
        //if (collision.gameObject.CompareTag(tag)) return;

        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(BASE_DAMAGE * stats.damageModifier);
        }
    }

    IEnumerator RageCoroutine()
    {
        isInRage = true;
        canEnterRage = false;

        // Multiply speed by projectileSpeed
        currentSpeed = stats.speedModifier * stats.projectilSpeedModifier;

        // Wait for duration to end rage
        yield return new WaitForSeconds(stats.durationModifier);

        // Return to baseSpeed
        currentSpeed = stats.speedModifier;
        isInRage = false;

        // Wait for the end of the cooldown before allowing rage again
        yield return new WaitForSeconds(stats.cooldownModifier);
        canEnterRage = true;
    }

    protected override void HandleDeath()
    {
        gameObject.SetActive(false);
        GameManager.SetGameState(GameState.GameOver);
    }
}
