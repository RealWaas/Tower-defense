using System;
using UnityEngine;

public static class UpgradeManager
{

    private static bool isTurretSelected = false;

    public static TurretData selectedTurret { get; private set; }

    public static event Action OnUpgradeSelected;
    public static event Action OnUpgradeDeselected;

    public static void SelectTurret(TurretData _selectedTurret)
    {
        selectedTurret = _selectedTurret;

        isTurretSelected = true;
        OnUpgradeSelected?.Invoke();
    }

    public static void DeselectTurret()
    {
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
    // TODO
    // Handle the upgrade UI of each turrets
}
