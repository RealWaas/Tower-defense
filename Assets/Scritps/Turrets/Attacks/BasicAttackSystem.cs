using UnityEngine;

public class BasicAttackSystem : AttackSystem
{
    protected override void AttackEnemy(EnemyBehaviour _enemy)
    {
        GameObject projectile = PoolManager.GetAvailableObjectFromPool(projectilePrefab);
        projectile.transform.position = transform.position;

        projectile.GetComponent<Projectile>().InitBullet(_enemy, stats);
        nextAttackTimer = Time.time + AttackInterval;
    }
}
