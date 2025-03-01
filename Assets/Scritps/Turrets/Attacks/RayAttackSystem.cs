using System.Collections.Generic;
using UnityEngine;

public class RayAttackSystem : AttackSystem
{

    List<GameObject> laserList = new List<GameObject>();
    protected override void Update()
    {
        if (enemyInRange.Count == 0 || !projectilePrefab)
        {
            AttackEnemy(null);
            return;
        }

        EnemyBehaviour currentTarget = enemyInRange[0];

        if (currentTarget.isActiveAndEnabled == false)
        {
            enemyInRange.Remove(currentTarget);
        }

        RotateToward(currentTarget.transform.position);

        AttackEnemy(currentTarget);
    }

    protected override void AttackEnemy(EnemyBehaviour _enemy)
    {
        if(_enemy == null)
        {
            if(laserList.Count != 0)
            {
                foreach (GameObject laser in laserList)
                {
                    laser.gameObject.SetActive(false);
                }
                laserList.Clear();
            }
            return;
        }

        if(laserList.Count == 0)
        {
            foreach (Transform canon in canonList)
            {
                GameObject laser = PoolManager.GetAvailableObjectFromPool(projectilePrefab);
                laser.transform.parent = canon;
                laser.transform.rotation = canon.rotation;
                laser.transform.position = canon.position;

                laser.GetComponent<Laser>().InitBullet(_enemy, stats);

                laserList.Add(laser);
            }
        }
    }
}
