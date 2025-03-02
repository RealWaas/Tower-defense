using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private TMP_Text UpgradePrice;

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
        {
            TurretSO turretSO = ItemDrawer.instance.GetTurretSO(turretData.turretType);

            // Show price
            int turretPrice = turretSO.turretStats[turretData.turretLevel + 1].upgradePrice;
            UpgradePrice.text = turretPrice.ToString();

            // Set interactable if buyable
            button.interactable = CurrencyManager.instance.IsBuyable(turretPrice);
        }
    } 
    private void SetNonInteractable()
    {
        UpgradePrice.text = "--";
        button.interactable = false;
    }
    
    public void OnClick()
    {
        if (!GameManager.isAlive)
            return;

        UpgradeManager.UpgradeTurret();
    }
}
