using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] AttackBehaviourSO attackBehaviour;
    [SerializeField] Entity attacker;

    [SerializeField] protected float cooldownTimer;
    float nextAttackTimer;

    //Attack per seconds
    //float AttackInterval => 1f / cooldownTimer;

    [ContextMenu("init")]
    private void InitWeapon()
    {
        if (attackBehaviour != null)
            cooldownTimer = attackBehaviour.baseCooldown;
        nextAttackTimer = cooldownTimer;
    }

    private void Update()
    {
        if (Time.time >= nextAttackTimer && nextAttackTimer != 0)
        {
            PerformAttack();
            nextAttackTimer = Time.time + cooldownTimer;
            
            //Attack per seconds
            //nextAttackTimer = Time.time + AttackInterval;
        }
    }

    void PerformAttack()
    {
        attackBehaviour.ExecuteAttack(attacker);
    }
}
