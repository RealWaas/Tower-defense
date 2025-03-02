using UnityEngine;
using static GridManager;

[RequireComponent(typeof(HealthSystem))]
public abstract class EnemyBehaviour : MonoBehaviour
{
    protected HealthSystem healthSystem;

    [SerializeField] protected EnemyDataSO enemyData;

    [SerializeField] protected float slowEffect = 1;
    protected float slowTimer = 0;
    protected float slowCooldown = 2f;

    [SerializeField] protected GameObject deathEffect;
    public int indexOnPath { get; protected set; } = 0;

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnHealthEmpty += HandleDeath;
        healthSystem.OnSlow += TakeSlow;
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthEmpty -= HandleDeath;
        healthSystem.OnSlow -= TakeSlow;
    }

    protected void OnEnable()
    {
        InitEnemy();
    }

    protected virtual void InitEnemy()
    {
        healthSystem.ResetHealth(enemyData.enemyHealth * (WaveManager.instance.currentWaveIndex * .2f));
    }

    protected virtual void Update()
    {
        if(enemyData == null)
            return;

        Vector3 targetPos = GetRelativePoint(mainPath[indexOnPath]);
        targetPos.y = transform.position.y;

        Vector3 targetDir = targetPos - transform.position;

        RotateToward(targetPos);

        if(slowEffect != 1 && Time.time >= slowTimer)
        {
            slowEffect = 1;
        }

        if (targetDir.magnitude <= 0.1f)
            OnTargetReached();

        transform.position += targetDir.normalized * Time.deltaTime * (enemyData.enemySpeed * slowEffect);
    }
    protected void RotateToward(Vector3 _targetPos)
    {
        _targetPos.y = transform.position.y;

        transform.rotation = Quaternion.LookRotation((_targetPos - transform.position).normalized);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != this.tag)
        {
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(enemyData.enemyDamages);
            }
        }
    }

    protected virtual void OnTargetReached()
    {
        if(indexOnPath +1 >=  mainPath.Count)
        {
            HandleDeath();
        }
        else
            indexOnPath ++;
    }

    protected virtual void TakeSlow(float _slowAmount)
    {
        slowEffect = _slowAmount;
        slowTimer = Time.time + slowCooldown;
    }

    protected virtual void HandleDeath()
    {
        WaveManager.instance.OnEnemyDeath();

        CurrencyManager.instance.AddMoney(enemyData.enemyMoney);

        GameObject effect = PoolManager.GetAvailableObjectFromPool(deathEffect);
        effect.transform.position = transform.position;

        gameObject.SetActive(false);
    }
}
