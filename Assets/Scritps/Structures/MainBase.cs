using UnityEngine;

[RequireComponent (typeof(HealthSystem))]
public class MainBase : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnHealthEmpty += HandleDeath;
        UI_HealthDisplayer.instance.SetHealthSystem(healthSystem);
    }

    private void Start()
    {
        healthSystem.ResetHealth(20);
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthEmpty -= HandleDeath;
    }

    private void HandleDeath()
    {
        Debug.Log("looser");
        Destroy(gameObject);
    }
}
