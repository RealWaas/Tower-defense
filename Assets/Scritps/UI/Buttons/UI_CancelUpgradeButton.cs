using UnityEngine;
using UnityEngine.UI;

public class UI_CancelUpgradeButton : MonoBehaviour
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

    private void SetInteractable() => button.interactable = true;
    private void SetNonInteractable() => button.interactable = false;

    public void OnClick()
    {
        UpgradeManager.DeselectTurret();
    }
}
