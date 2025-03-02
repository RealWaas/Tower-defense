using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TurretPlacementButton : MonoBehaviour
{
    [SerializeField] private TurretSO turretSO;

    [SerializeField] private TMP_Text turretName;
    [SerializeField] private TMP_Text turretPrice;

    [SerializeField] private Image turretIcon;

    [SerializeField] private Button selectButton;

    private void Start()
    {
        selectButton.onClick.AddListener(OnClick);
        CurrencyManager.instance.OnMoneyUpdate.AddListener(CheckBuyPossibility);
    }

    private void OnDestroy()
    {
        selectButton.onClick.RemoveListener(OnClick);
        CurrencyManager.instance.OnMoneyUpdate.RemoveListener(CheckBuyPossibility);
    }

    public void SetContent(TurretSO _turretSO)
    {
        turretSO = _turretSO;
        turretName.text = turretSO.turretName;
        turretPrice.text = turretSO.turretStats[0].upgradePrice.ToString();
        turretIcon.sprite = turretSO.turretIcon;
        CheckBuyPossibility();
    }

    public void OnClick()
    {
        if (!turretSO) return;
        PlacementManager.instance.SetSelectedInstance(turretSO.turretType);
    }

    private void CheckBuyPossibility(int _money = 0)
    {
        selectButton.interactable = CurrencyManager.instance.IsBuyable(turretSO.turretStats[0].upgradePrice);
    }
}
