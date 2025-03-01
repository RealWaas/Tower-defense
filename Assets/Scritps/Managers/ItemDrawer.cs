using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDrawer : MonoBehaviour
{
    public static ItemDrawer instance;

    public List<TurretSO> turretList = new List<TurretSO>();

    private void Awake()
    {
        instance = this;
    }

    private List<TurretType> GetAllAvailableTurret()
    {
        List<TurretSO> availableTurrets = turretList.Where(e =>
        {
            return !InventoryManager.availableTurrets.Contains(e.turretType);
        }).ToList();

        List<TurretType> availableTypes = new List<TurretType>();

        foreach (TurretSO turret in availableTurrets)
        {
            availableTypes.Add(turret.turretType);
        }

        return availableTypes;
    }

    public List<TurretType> GetRandomTurrets(int _amount)
    {
        List<TurretType> availableTurrets = GetAllAvailableTurret();
        availableTurrets = ShuffleList(availableTurrets);

        List<TurretType> randomTurrets = new List<TurretType>();

        for (int index = 0; index <= _amount - 1; index++)
        {
            if (index >= availableTurrets.Count)
            {
                continue;
            }
            randomTurrets.Add(availableTurrets[index]);
        }

        return randomTurrets;
    }

    private List<T> ShuffleList<T>(List<T> _listToShuffle)
    {
        for (int indexInList = _listToShuffle.Count - 1; indexInList >= 0; indexInList--)
        {
            int randomIndex = Random.Range(0, indexInList + 1);
            var temp = _listToShuffle[indexInList];
            _listToShuffle[indexInList] = _listToShuffle[randomIndex];
            _listToShuffle[randomIndex] = temp;
        }

        return _listToShuffle;
    }

    public GameObject GetTurretPrefab(TurretType _type, int _level)
    {
        TurretSO turret = turretList.Where(e => {
            return e.turretType == _type;
        }).FirstOrDefault();

        return turret.turretStats[_level].turretPrefab;
    }

    public TurretStats GetTurretStats(TurretType _type, int _level)
    {
        TurretSO turret = turretList.Where(e => {
            return e.turretType == _type;
        }).FirstOrDefault();

        TurretStats newStats = new TurretStats(turret.turretStats[_level]);

        return newStats;
    }

    public int GetMaxLevel(TurretType _type)
    {
        TurretSO turret = turretList.Where(e => {
            return e.turretType == _type;
        }).FirstOrDefault();

        return turret.turretStats.Count();
    }

    public TurretSO GetTurretSO(TurretType _type)
    {
        return turretList.Where(e => {
            return e.turretType == _type;
        }).FirstOrDefault();
    }
}
