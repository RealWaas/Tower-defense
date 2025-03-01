using UnityEngine;
using static GridManager;

[RequireComponent(typeof(HealthSystem))]
public class EnemyBehaviour : MonoBehaviour
{
    private HealthSystem healthSystem;

    [SerializeField] private GameObject deathEffect;
    public int indexOnPath { get; private set; } = 0;
    [SerializeField] private float moveSpeed = 2;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnHealthEmpty += HandleDeath;

        healthSystem.ResetHealth(10);
    }

    private void Update()
    {
        Vector3 targetDir = GetRelativePoint(mainPath[indexOnPath]) - transform.position;

        if (targetDir.magnitude <= 0.1f)
            OnTargetReached();

        transform.position += targetDir.normalized * Time.deltaTime * moveSpeed;
    }

    private void OnTargetReached()
    {
        if(indexOnPath +1 >=  mainPath.Count)
        {
            HandleDeath();
        }
        else
            indexOnPath ++;
    }

    private void HandleDeath()
    {
        WaveManager.instance.OnEnemyDeath();

        GameObject effect = PoolManager.GetAvailableObjectFromPool(deathEffect);
        effect.transform.position = transform.position;

        gameObject.SetActive(false);
    }
}
