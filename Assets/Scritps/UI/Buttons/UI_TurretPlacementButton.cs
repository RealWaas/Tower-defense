using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TurretPlacementButton : MonoBehaviour
{
    [SerializeField] private TurretSO turretSO;

    [SerializeField] private TMP_Text turretName;

    [SerializeField] private Image turretIcon;

    public void SetContent(TurretSO _turretSO)
    {
        turretSO = _turretSO;
        turretName.text = turretSO.turretName;
        turretIcon.sprite = turretSO.turretIcon;
    }

    public void OnClick()
    {
        if (!turretSO) return;
        PlacementManager.instance.SetSelectedTurret(turretSO.turretType);
    }
}
