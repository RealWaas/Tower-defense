using System;
using System.Collections.Generic;
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

        GameManager.OnWaveStart += DeselectTurret;
        UpgradeManager.OnUpgradeSelected += DeselectTurret;
    }

    private void OnDestroy()
    {
        GameManager.OnWaveStart -= DeselectTurret;
        UpgradeManager.OnUpgradeSelected -= DeselectTurret;
    }

    public void DeselectTurret()
    {
        selectedPlacer = null;
        Destroy(turretInstance);
        turretInstance = null;
        selectedType = TurretType.None;
    }
    public void SetSelectedTurret(TurretType _selectedType)
    {
        if(selectedType == _selectedType)
        {
            DeselectTurret();
            return;
        }

        UpgradeManager.DeselectTurret();

        if (turretInstance)
            Destroy(turretInstance);

        selectedType = _selectedType;

        GameObject turretPrefab = ItemDrawer.instance.GetTurretPrefab(selectedType, 0);
        
        turretInstance = Instantiate(turretPrefab);
        turretInstance.GetComponent<TurretVisual>().SetVisualizerMaterial();
        
        HideTurret();
    }

    public void ShowTurret(TurretPlacer _placer)
    {
        selectedPlacer = _placer;
        turretInstance.SetActive(true);
        turretInstance.transform.position = _placer.transform.position;
    }
    public void HideTurret()
    {
        selectedPlacer = null;
        turretInstance.SetActive(false);
    }

    void Update()
    {
        if (GameManager.isInWave)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

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
                    ShowTurret(_placer);
                    return;
                }
            }
            if (selectedPlacer != null)
                HideTurret();
        }
        if (Input.GetMouseButtonDown(0))
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

    private void PlaceTurret()
    {
        TurretData newData = new TurretData(selectedType, selectedPlacer.placerPosition, 0);

        GameObject newTurret = Instantiate(turretMainPrefab);
        newTurret.transform.position = selectedPlacer.transform.position;

        turretInstance.GetComponent<TurretVisual>().SetBaseMaterial();

        TurretMain turretMain = newTurret.GetComponent<TurretMain>();
        turretMain.InitTurret(newData);

        placedTurretList.Add(turretMain.turretData);
        placedTurretObjects.Add(turretMain);

        selectedPlacer.SetTurret();

        DeselectTurret();
    }

    public void UpdtateTurretLevel(TurretData _turret)
    {
        TurretData updatedTurret = new TurretData(_turret);

        placedTurretList[GetIndexOfTurret(_turret)] = updatedTurret;
    }

    public TurretMain GetTurretMain(TurretData _turret)
    {
        return placedTurretObjects[GetIndexOfTurret(_turret)];
    }
    private int GetIndexOfTurret(TurretData _turret)
    {
        for (int turretIndex = 0; turretIndex <= placedTurretList.Count - 1; turretIndex++)
            if (_turret == placedTurretList[turretIndex])
                return turretIndex;

        Debug.LogError("out of range");
        return -1;
    }

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
