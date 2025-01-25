using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/new Enemy type")]
public class EnemyData : ScriptableObject
{
    public int maxHealth;
    public float moveSpeed;
    public int damage;
}
