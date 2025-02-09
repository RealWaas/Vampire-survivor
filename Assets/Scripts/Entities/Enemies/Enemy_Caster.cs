using UnityEngine;

public class Enemy_Caster : BaseEnemy
{
    [SerializeField] private float maxPlayerDist = 5f;
    protected override void Move(Vector2 _playerDir)
    {
        base.Move(_playerDir);

        enemyRenderer.flipX = _playerDir.x < 0;
        if (_playerDir.magnitude <= maxPlayerDist)
            _playerDir = Vector3.zero;

        movementHandler.MoveEntity(_playerDir.normalized * BASE_SPEED * stats.speedModifier);
    }
}
