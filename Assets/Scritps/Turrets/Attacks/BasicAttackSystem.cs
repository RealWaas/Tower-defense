using UnityEngine;

public class BasicAttackSystem : AttackSystem
{
    private int canonIndex = 0;
    protected override void AttackEnemy(EnemyBehaviour _enemy)
    {
        GameObject projectile = PoolManager.GetAvailableObjectFromPool(projectilePrefab);
        projectile.transform.position = canonList[canonIndex].position;

        projectile.GetComponent<Projectile>().InitBullet(_enemy, stats);


        nextAttackTimer = Time.time + (AttackInterval / canonList.Count);

        canonIndex++;
        if (canonIndex >= canonList.Count)
            canonIndex = 0;
    }
}
