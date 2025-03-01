using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        UpgradeManager.OnUpgradeSelected += SetInteractable;
        UpgradeManager.OnUpgradeDeselected += SetNonInteractable;

        SetNonInteractable();
    }

    private void OnDestroy()
    {
        UpgradeManager.OnUpgradeSelected -= SetInteractable;
        UpgradeManager.OnUpgradeDeselected -= SetNonInteractable;
    }

    private void SetInteractable()
    {
        TurretData turretData = UpgradeManager.selectedTurret;
        int maxLevel = ItemDrawer.instance.GetMaxLevel(turretData.turretType);

        // Make the button interactable only if the turret can be upgraded
        if (turretData.turretLevel < maxLevel - 1)
            button.interactable = true;
    } 
    private void SetNonInteractable() => button.interactable = false;
    
    public void OnClick()
    {
        UpgradeManager.UpgradeTurret();
    }
}
