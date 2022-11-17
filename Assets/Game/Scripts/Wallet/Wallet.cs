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
        _saveManager.LoadSave();
        SetMoney(_saveManager.Save.MoneyAmount);
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

    private void SaveMoney(int amount)
    {
        _saveManager.Save.MoneyAmount = amount;
        _saveManager.SaveData();
        //PlayerPrefs.SetInt(_moneyString, amount);
    }

}
