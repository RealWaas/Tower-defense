using System;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    public int baseMoney;

    private int Money;
    public int money
    {
        get => Money;
        private set
        {
            Money = value;
            OnMoneyUpdate?.Invoke(Money);
        }
    }

    public UnityEvent<int> OnMoneyUpdate;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        money = baseMoney;
    }

    public void AddMoney(int _amount) => money += _amount;

    public void RemoveMoney(int _amount) => money -= _amount;

    public void SetMoney(int _amount) => money = _amount;

    public bool IsBuyable(int _price) => money - _price >= 0;
}
