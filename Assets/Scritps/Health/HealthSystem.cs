using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable
{
    public event Action OnHealthUpdated;
    public event Action OnHealthEmpty;
    public event Action<float> OnSlow;

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


    [SerializeField] private ParticleSystem lowHealthEffect;

    public void SetHealth(float _health)
    {
        if(_health == 0)
        {
            health = maxHealth;
            return;
        }
        health = _health;
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

        if (health < maxHealth / 2)
            lowHealthEffect.Play();

        if (health <= 0)
        {
            isVulnerable = false;
            lowHealthEffect.Stop();
            OnHealthEmpty?.Invoke();
        }
    }

    public void TakeSlow(float _slowSpeed) => OnSlow?.Invoke(_slowSpeed);
}
