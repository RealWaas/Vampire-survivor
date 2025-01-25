using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] AttackBehaviourSO attackBehaviour;
    [SerializeField] Entity attacker;

    protected float cooldownTimer;
    float nextAttackTimer;

    float AttackInterval => 1f / cooldownTimer;

    [ContextMenu("init")]
    private void InitWeapon()
    {
        if (attackBehaviour != null) cooldownTimer = attackBehaviour.cooldown;
        nextAttackTimer = cooldownTimer;
    }

    private void Update()
    {
        if (Time.time >= nextAttackTimer && nextAttackTimer != 0)
        {
            PerformAttack();
            nextAttackTimer = Time.time + AttackInterval;
        }
    }

    void PerformAttack()
    {
        attackBehaviour.ExecuteAttack(attacker);
    }
}
