using UnityEngine;
using UnityEngine.UI;

public class UI_RemoveTurretButton : MonoBehaviour
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
        if (!GameManager.isAlive)
            return;

        UpgradeManager.RemoveTurret();
    }
}
