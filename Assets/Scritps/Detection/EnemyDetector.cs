using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EnemyDetector
{
    public static EnemyBehaviour GetClosestEnemy(Vector3 _origin, float _radius, LayerMask _mask)
    {
        List<EnemyBehaviour> enemiesInRange = GetAllEnemies(_origin, _radius, _mask);

        return enemiesInRange.OrderBy(e =>
        {
            return e.indexOnPath;
        }).FirstOrDefault();
    }

    private static List<EnemyBehaviour> GetAllEnemies(Vector3 _origin, float _radius, LayerMask _mask)
    {
        Collider[] collidersInRange = Physics.OverlapSphere(_origin, _radius, _mask);

        // No Enemy in range
        if (collidersInRange.Length == 0)
            return null;


        List<EnemyBehaviour> enemiesInRange = new List<EnemyBehaviour>();

        foreach (Collider collider in collidersInRange)
        {
            enemiesInRange.Add(collider.GetComponent<EnemyBehaviour>());
        }

        return enemiesInRange;
    }
}
