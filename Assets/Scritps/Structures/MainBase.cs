using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent (typeof(HealthSystem))]
public class MainBase : MonoBehaviour
{
    public static MainBase instance;

    [SerializeField] private float baseMaxHealth;

    private HealthSystem healthSystem;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private ParticleSystem deathEffect2;

    private void Awake()
    {
        instance = this;

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnHealthEmpty += HandleDeath;
        UI_HealthDisplayer.instance.SetHealthSystem(healthSystem);
    }

    private void Start()
    {
        healthSystem.ResetHealth(baseMaxHealth);
        SetHealth(DataManager.GetMainBaseHp());
    }

    private void SetHealth(float _health)
    {
        healthSystem.SetHealth(_health);
    }

    public float GetHealth()
    {
        return healthSystem.health;
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthEmpty -= HandleDeath;
    }

    private void HandleDeath()
    {
        Debug.Log("looser");
        deathEffect.Play();
        deathEffect2.Play();
        //Destroy(gameObject);
    }
}
