using System.Collections.Generic;
using UnityEngine;

public class UI_ItemChoicePanel : MonoBehaviour
{
    [SerializeField] private GameObject _itemChoicePrefab;

    private const int MAX_CHOICES = 3;

    public void Awake()
    {
        PreparationManager.OnItemChoice += SetItemChoices;
    }

    public void OnDestroy()
    {
        PreparationManager.OnItemChoice -= SetItemChoices;
    }

    private void SetItemChoices()
    {
        RemoveChoices();

        if (WaveManager.instance.currentWaveIndex % PreparationManager.UNLOCK_WAVES_INDEX == 0)
        {
            InstantiateChoices(ItemDrawer.instance.GetRandomTurrets(MAX_CHOICES));
        }
    }

    private void InstantiateChoices(List<TurretType> _choiceList)
    {
        foreach (TurretType choice in _choiceList)
        {
            TurretSO turretSO = ItemDrawer.instance.GetTurretSO(choice);
            GameObject choiceInstance = Instantiate(_itemChoicePrefab, transform);
            choiceInstance.GetComponent<UI_ItemChoiceButton>().SetContent(turretSO);
        }
    }

    private void RemoveChoices()
    {
        foreach(Transform child in transform)
            Destroy(child.gameObject);
    }
}
