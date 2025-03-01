using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class AttackSystem : MonoBehaviour
{
    [SerializeField] protected List<EnemyBehaviour> enemyInRange = new List<EnemyBehaviour>();
    [SerializeField] protected List<Transform> canonList = new List<Transform>();

    protected TurretStats stats;
    [SerializeField] protected GameObject projectilePrefab;

    protected float nextAttackTimer = 0;
    protected float AttackInterval => 1f / stats.attackSpeed;

    protected SphereCollider rangeCollider;

    // TODO
    // Check every X frames to target a different enemy
    // Time.FrameCount % 60 == 0;

    protected void Awake()
    {
        rangeCollider = GetComponent<SphereCollider>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyBehaviour enemy))
            enemyInRange.Add(enemy);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyBehaviour enemy))
            enemyInRange.Remove(enemy);
    }

    protected virtual void Update()
    {
        if (enemyInRange.Count == 0 || !projectilePrefab)
            return;

        EnemyBehaviour currentTarget = enemyInRange[0];

        if (currentTarget.isActiveAndEnabled == false)
        {
            enemyInRange.Remove(currentTarget);
        }

        RotateToward(currentTarget.transform.position);

        //Attack per seconds
        if (Time.time >= nextAttackTimer && nextAttackTimer != 0)
        {
            AttackEnemy(currentTarget);
        }
    }

    protected abstract void AttackEnemy(EnemyBehaviour _enemy);
    protected void RotateToward(Vector3 _targetPos)
    {
        _targetPos.y = transform.position.y;

        transform.rotation = Quaternion.LookRotation((_targetPos - transform.position).normalized);
    }

    public void InitAttack(TurretStats _turretStats, GameObject _projectilePrefab)
    {
        stats = _turretStats;
        projectilePrefab = _projectilePrefab;
        SetCooldown();
    }

    protected void SetCooldown() => nextAttackTimer = Time.time;

    protected void OnDrawGizmos()
    {
        if (!rangeCollider)
            return;

        Gizmos.DrawWireSphere(transform.position, rangeCollider.radius);
    }
}
