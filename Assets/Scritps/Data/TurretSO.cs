using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Turret/new Turret")]
public class TurretSO : ScriptableObject
{
    public string turretName;
    public Sprite turretIcon;
    public List<TurretStats> turretStats;
    public TurretType turretType;
}

[Serializable]
public class TurretStats
{
    public TurretStats(TurretStats _stats)
    {
        attackSpeed = _stats.attackSpeed;

        bulletDamage = _stats.bulletDamage;
        bulletSpeed = _stats.bulletSpeed;

        turretPrefab = _stats.turretPrefab;
        projectilePrefab = _stats.projectilePrefab;
    }

    public string levelDescription;
    public GameObject turretPrefab;
    public GameObject projectilePrefab;

    public int upgradePrice;

    public float attackSpeed = 1;

    public float bulletDamage = 1;
    public float bulletSpeed = 1;
}
