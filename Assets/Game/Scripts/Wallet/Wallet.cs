using UnityEngine;
using System;
using Zenject;

public class Wallet : MonoBehaviour
{

    public Action ButtonPushEvent;
    public Action<int> AmountChangeEvent;

    [SerializeField] private int _startAmount;
    private SaveManager _saveManager;
    private int _money;
    private string _moneyString = "WalletAmount";

    [Inject]
    private void Construct(SaveManager saveManager)
    {
        _saveManager = saveManager;
    }

    private void Awake()
    {
        //   SetMoney(PlayerPrefs.GetInt(_moneyString, 0));
        _saveManager.Load();
        SetMoney(_saveManager.SaveData.MoneyAmount);
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

        ButtonPushEvent?.Invoke();

        _money -= amount;
        AmountChangeEvent?.Invoke(_money);
        return true;
    }

    public bool CheckMoney(int amount)
    {
        if (amount > _money) return false;

        return true;
    }

    private void SaveMoney(int amount)
    {
        _saveManager.SaveData.MoneyAmount = amount;
        _saveManager.Save();
        //PlayerPrefs.SetInt(_moneyString, amount);
    }

}
