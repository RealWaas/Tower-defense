using UnityEngine;
using UnityEngine.UI;

public class UI_PlacementSelection : MonoBehaviour
{
    [SerializeField] GameObject placementButtonPrefab;
    private Button selectionButton;

    public void Awake()
    {
        selectionButton = GetComponent<Button>();
        PreparationManager.OnPreparation += SetPlacementSelection;
    }

    public void OnDestroy()
    {
        PreparationManager.OnPreparation -= SetPlacementSelection;   
    }

    public void SetPlacementSelection()
    {
        RemoveButtons();

        foreach (TurretType turrets in InventoryManager.availableTurrets)
        {
            TurretSO turretSO = ItemDrawer.instance.GetTurretSO(turrets);
            GameObject placementButton = Instantiate(placementButtonPrefab, transform);
            placementButton.GetComponent<UI_TurretPlacementButton>().SetContent(turretSO);
        }
    }

    private void RemoveButtons()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
