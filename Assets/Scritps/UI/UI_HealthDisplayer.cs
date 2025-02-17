using UnityEngine;
using UnityEngine.UI;

public class UI_HealthDisplayer : MonoBehaviour
{
    public static UI_HealthDisplayer instance;
    private HealthSystem healthSystem;

    [SerializeField] private Image healthBar;

    private void Awake()
    {
        instance = this;
    }
    public void SetHealthSystem(HealthSystem _healthSystem)
    {
        healthSystem = _healthSystem;
        healthSystem.OnHealthUpdated += UpdateHealth;
    }

    private void OnDestroy()
    {
        if (!healthSystem) return;
        healthSystem.OnHealthUpdated -= UpdateHealth;
    }

    public void UpdateHealth()
    {
        healthBar.fillAmount = healthSystem.health / healthSystem.maxHealth;
    }
}
