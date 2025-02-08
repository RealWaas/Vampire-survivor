using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float _damage, float _knockBack = 0);
}