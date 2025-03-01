using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemChoiceButton : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private TurretSO turretSO;

    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text turretLevel;

    [SerializeField] private Image itemIcon;

    public void SetContent(TurretSO _turretSO)
    {
        turretSO = _turretSO;
        itemName.text = turretSO.turretName;
        itemIcon.sprite = turretSO.turretIcon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.AddTurretToInventory(turretSO.turretType);

        PreparationManager.SetPreparationPhase();
    }
}
