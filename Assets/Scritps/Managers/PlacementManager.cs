using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GridManager;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager instance;

    public static event Action OnTurretSelected;

    public List<TurretData> placedTurretList = new List<TurretData>();
    public List<TurretMain> placedTurretObjects = new List<TurretMain>();

    public GameObject turretInstance;
    public GameObject turretMainPrefab;
    public TurretType selectedType = TurretType.None;

    public TurretPlacer selectedPlacer;

    public LayerMask upgradeMask;
    public LayerMask placementMask;

    void Awake()
    {
        instance = this;

        GameManager.OnWaveStart += UnSelectInstance;
        UpgradeManager.OnUpgradeSelected += UnSelectInstance;
    }

    private void OnDestroy()
    {
        GameManager.OnWaveStart -= UnSelectInstance;
        UpgradeManager.OnUpgradeSelected -= UnSelectInstance;
    }

    public void UnSelectInstance()
    {
        selectedPlacer = null;
        Destroy(turretInstance);
        turretInstance = null;
        selectedType = TurretType.None;
    }

    public void SetSelectedInstance(TurretType _selectedType)
    {
        if(selectedType == _selectedType)
        {
            UnSelectInstance();
            return;
        }

        UpgradeManager.DeselectTurret();

        if (turretInstance)
            Destroy(turretInstance);

        selectedType = _selectedType;

        GameObject turretPrefab = ItemDrawer.instance.GetTurretPrefab(selectedType, 0);
        
        turretInstance = Instantiate(turretPrefab);
        turretInstance.GetComponent<TurretVisual>().SetPlacementMaterial();
        
        HideTurretInstance();
    }

    public void ShowTurretInstance(TurretPlacer _placer)
    {
        selectedPlacer = _placer;
        turretInstance.SetActive(true);
        turretInstance.transform.position = _placer.transform.position;
    }

    public void HideTurretInstance()
    {
        selectedPlacer = null;
        turretInstance.SetActive(false);
    }

    void Update()
    {
        if (GameManager.isInWave || !GameManager.isAlive) // Cancel if not in preparation phase
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // If the player is trying to place a turret
        if (turretInstance)
        {
            if (Physics.Raycast(ray, out hit, 50, placementMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out TurretPlacer _placer))
                {
                    if (Input.GetMouseButtonDown(0))
                        PlaceTurret();

                    if (_placer == selectedPlacer || _placer.hasTurret)
                        return;
                    ShowTurretInstance(_placer);
                    return;
                }
            }
            if (selectedPlacer != null)
                HideTurretInstance();
        }

        // If no turret is beign placed, select one on click
        else if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 50, upgradeMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out TurretMain _turret))
                {
                    UpgradeManager.SelectTurret(_turret.turretData);
                }
            }
        }
    }

    /// <summary>
    /// Place a turret in the level an initialize it.
    /// </summary>
    private void PlaceTurret()
    {
        TurretData newData = new TurretData(selectedType, selectedPlacer.placerPosition, 0);

        TurretSO turretSO = ItemDrawer.instance.GetTurretSO(selectedType);

        CurrencyManager.instance.RemoveMoney(turretSO.turretStats[0].upgradePrice);

        GameObject newTurret = Instantiate(turretMainPrefab);
        newTurret.transform.position = selectedPlacer.transform.position;

        turretInstance.GetComponent<TurretVisual>().SetBaseMaterial();

        TurretMain turretMain = newTurret.GetComponent<TurretMain>();
        turretMain.InitTurret(newData);

        placedTurretList.Add(turretMain.turretData);
        placedTurretObjects.Add(turretMain);

        selectedPlacer.SetTurret();

        UnSelectInstance();
    }

    public void RemoveTurret(TurretData _turretData)
    {
        TurretMain turretMain = GetTurretMain(_turretData);

        placedTurretList.Remove(_turretData);
        placedTurretObjects.Remove(turretMain);

        Destroy(turretMain.gameObject);

        TurretPlacer turretPlacer = placerList.Where(e => { return e.placerPosition == _turretData.turretPos; }).First();

        turretPlacer.SetTurret(false);
    }

    /// <summary>
    /// Update the data of the upgraded Turret to match its current level.
    /// </summary>
    /// <param name="_turret"></param>
    public void UpdtateTurretLevel(TurretData _turret)
    {
        TurretData updatedTurret = new TurretData(_turret);

        int turretIndex = GetIndexOfTurret(_turret);

        // Remove upgrade price from Currency Manager
        TurretSO turretSO = ItemDrawer.instance.GetTurretSO(_turret.turretType);

        CurrencyManager.instance.RemoveMoney(turretSO.turretStats[_turret.turretLevel].upgradePrice);

        placedTurretList[turretIndex] = updatedTurret;
    }

    /// <summary>
    /// Return the TurretMain relatif to the right Data.
    /// </summary>
    /// <param name="_turret"></param>
    /// <returns></returns>
    public TurretMain GetTurretMain(TurretData _turret) => placedTurretObjects[GetIndexOfTurret(_turret)];

    /// <summary>
    /// Get the index of a turret in the list of placedTurrets.
    /// </summary>
    /// <param name="_turret"></param>
    /// <returns></returns>
    private int GetIndexOfTurret(TurretData _turret)
    {
        for (int turretIndex = 0; turretIndex <= placedTurretList.Count - 1; turretIndex++)
            if (_turret == placedTurretList[turretIndex])
                return turretIndex;

        Debug.LogError("out of range");
        return -1;
    }

    /// <summary>
    /// Load all turrets from a list in the level.
    /// </summary>
    /// <param name="_turretList"></param>
    public void LoadAllTurrets(List<TurretData> _turretList)
    {
        foreach(TurretData turret in _turretList)
        {
            TurretData newData = new TurretData(turret);

            GameObject newTurret = Instantiate(turretMainPrefab);
            newTurret.transform.position = GetRelativePoint(newData.turretPos);

            TurretMain turretMain = newTurret.GetComponent<TurretMain>();
            
            placedTurretList.Add(newData);
            placedTurretObjects.Add(turretMain);

            turretMain.InitTurret(newData);
        }
    }
}
