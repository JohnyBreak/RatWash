using UnityEngine;
using System;

public class Wallet : MonoBehaviour
{
    public Action<int> AmountChangeEvent;
    [SerializeField] private int _startAmount;
    private int _money;
    private string _moneyString = "WalletAmount";

    private void Awake()
    {
        SetMoney(PlayerPrefs.GetInt(_moneyString, 0));
        AmountChangeEvent += SaveMoney;
    }

    private void OnDestroy()
    {
        AmountChangeEvent -= SaveMoney;
    }

    public void SetMoney(int amount) 
    {
        _money = amount;
        AmountChangeEvent?.Invoke(_money);
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        AmountChangeEvent?.Invoke(_money);
    }

    public bool RemoveMoney(int amount)
    {
        if (amount > _money) return false;

        _money -= amount;
        AmountChangeEvent?.Invoke(_money);
        return true;
    }

    private void SaveMoney(int amount)
    {
        PlayerPrefs.SetInt(_moneyString, amount);
    }

}
