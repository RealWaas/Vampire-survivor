using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable
{
    public event Action OnHealthUpdated;
    public event Action OnHealthEmpty;
    public event Action<float> OnTakeDamage;
    public float maxHealth { get; private set; }

    private float Health;
    public float health
    {
        get => Health;
        protected set
        {
            Health = value;
            OnHealthUpdated?.Invoke();
        }
    }

    //public int damagaReduction;

    public void SetMaxHealt(float _maxHealth, float _healthModifier)
    {
        maxHealth = _maxHealth * _healthModifier;
        health *= _healthModifier;
    }
    public void ResetHealth(float _maxHealth)
    {
        maxHealth = _maxHealth;
        health = maxHealth;
    }

    public void TakeHeal(float _heal)
    {
        health += _heal;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    /// <summary>
    /// Reduce health by the damages taken and notify the entity of the knockback to apply.
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_knockBack"></param>
    public void TakeDamage(float _damage, float _knockBack = 0)
    {
        health -= _damage;
        OnTakeDamage?.Invoke(_knockBack);

        if (health <= 0)
        {
            OnHealthEmpty?.Invoke();
        }
    }
}
