using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Attacks/new Attack")]
public class AttackBehaviourSO : ScriptableObject
{
    [SerializeField] GameObject attackPrefab;
    [SerializeField] Vector3 attackOffset;


    public int damage;
    public float areaSize;
    public float duration;
    public float speed;

    public int piercing;

    public float cooldown;

    public void ExecuteAttack(Entity _attacker)
    {
        GameObject attack = Instantiate(attackPrefab, _attacker.transform.position + attackOffset, _attacker.transform.rotation);

        attack.transform.localPosition = attackOffset;

        if (attack.TryGetComponent(out AttackController attackController))
        {
            attackController.InitializeAttack(_attacker, this);
        }
    }
}
