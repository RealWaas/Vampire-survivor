using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Attacks/new Attack")]
public class AttackBehaviourSO : ScriptableObject
{
    [SerializeField] GameObject attackPrefab;
    [SerializeField] Vector3 attackOffset;


    public int baseDamage;
    public int baseAreaSize = 1;
    public float baseDuration;
    public float baseProjectileSpeed;

    public int basePiercing;

    public float baseCooldown;

    /// <summary>
    /// Instantiate the attack and initialize its controls.
    /// </summary>
    /// <param name="_attacker"></param>
    public void ExecuteAttack(Entity _attacker)
    {
        GameObject attack = Instantiate(attackPrefab, _attacker.transform.position /*+ attackOffset*/, _attacker.transform.rotation);
        //attack.transform.localPosition = attackOffset;

        // Geting its controller component
        if (attack.TryGetComponent(out AttackController attackController))
            attackController.InitializeAttack(_attacker, this);
    }
}
