using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable
{
    public event Action OnHealthUpdated;
    public event Action OnHealthEmpty;
    public int maxHealth { get; private set; }

    private int Health;
    public int health
    {
        get => Health;
        protected set
        {
            Health = value;
            OnHealthUpdated?.Invoke();
        }
    }

    //public int damagaReduction;

    public void GainMaxHealt(int _maxHealth)
    {
        maxHealth += _maxHealth;
        health += _maxHealth;
    }
    public void ResetHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
        health = maxHealth;
    }

    public void TakeHeal(int _heal)
    {
        health -= _heal;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if(health <= 0)
            OnHealthEmpty?.Invoke();
    }
}
