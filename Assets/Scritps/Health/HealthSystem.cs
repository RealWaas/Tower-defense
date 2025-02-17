using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable
{
    public event Action OnHealthUpdated;
    public event Action OnHealthEmpty;

    [SerializeField] protected bool isVulnerable = true;
    public float maxHealth { get; private set; }

    private float Health;
    public float health
    {
        get => Health;
        private set
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

    public void TakeDamage(float _damage)
    {
        if (!isVulnerable) return;

        health -= _damage;

        if (health <= 0)
            OnHealthEmpty?.Invoke();
    }
}
