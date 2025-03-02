using System;

public static class UpgradeManager
{

    private static bool isTurretSelected = false;

    public static TurretData selectedTurret { get; private set; }

    public static event Action OnUpgradeSelected;
    public static event Action OnUpgradeDeselected;

    public static void SelectTurret(TurretData _selectedTurret)
    {
        if(isTurretSelected)
            DeselectTurret();

        selectedTurret = _selectedTurret;

        isTurretSelected = true;
        OnUpgradeSelected?.Invoke();

        TurretMain turretMain = PlacementManager.instance.GetTurretMain(selectedTurret);
        turretMain.turretObject.GetChild(0).GetComponent<TurretVisual>().SetSelectionMaterial();
    }

    public static void DeselectTurret()
    {
        if (!isTurretSelected) return;


        TurretMain turretMain = PlacementManager.instance.GetTurretMain(selectedTurret);
        turretMain.turretObject.GetChild(0).GetComponent<TurretVisual>().SetBaseMaterial();

        isTurretSelected = false;
        OnUpgradeDeselected?.Invoke();
    }


    public static void UpgradeTurret()
    {
        if (!isTurretSelected)
            return;

        TurretMain turretMain = PlacementManager.instance.GetTurretMain(selectedTurret);
        
        turretMain.LevelUp();

        DeselectTurret();
    }

    public static void RemoveTurret()
    {
        PlacementManager.instance.RemoveTurret(selectedTurret);
        isTurretSelected = false;
        OnUpgradeDeselected?.Invoke();
    }
}
