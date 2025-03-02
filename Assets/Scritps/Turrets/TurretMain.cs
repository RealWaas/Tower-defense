using UnityEngine;

public class TurretMain : MonoBehaviour
{
    [SerializeField] public Transform turretObject;
    [SerializeField] private Transform turretLevels;
    public TurretData turretData { get; private set; }
    public TurretStats turretStats { get; private set; }

    public void SetTurretLevel(int _level)
    {
        turretData = new TurretData(turretData.turretType, turretData.turretPos, _level);

        turretStats = ItemDrawer.instance.GetTurretStats(turretData.turretType, _level);

        if(_level > 0)
            PlacementManager.instance.UpdtateTurretLevel(turretData);

        GameObject projectilePrefab = turretStats.projectilePrefab;

        for (int levelIndex = 0; levelIndex < ItemDrawer.instance.GetMaxLevel(turretData.turretType);  levelIndex++)
        {
            if(levelIndex <= _level)
                turretLevels.GetChild(levelIndex).gameObject.SetActive(true);
            else
                turretLevels.GetChild(levelIndex).gameObject.SetActive(false);

            if(levelIndex == _level)
            {
                GameObject prefab = turretStats.turretPrefab;
                foreach (Transform child in turretObject)
                    Destroy(child.gameObject);

                AttackSystem attackSystem = Instantiate(prefab, turretObject).GetComponent<AttackSystem>();
                attackSystem.InitAttack(turretStats, projectilePrefab);
            }
        }
    }

    public void LevelUp()
    {
        int maxLevel = ItemDrawer.instance.GetMaxLevel(turretData.turretType);

        if (turretData.turretLevel < maxLevel-1)
        {
            SetTurretLevel(turretData.turretLevel + 1);
        }
    }

    public void InitTurret(TurretData _data)
    {
        turretData = _data;
        SetTurretLevel(turretData.turretLevel);
    }
}
