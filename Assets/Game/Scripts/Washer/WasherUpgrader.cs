
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WasherUpgrader : MonoBehaviour
{
    [SerializeField] private List<WasherUpgradeButton> _upgradeButtons;
    private WasherStorage _washer;
    private Wallet _wallet;
    private SaveManager _saveManager;
    private int _upgradeLvl;
    private int _buttonIndex = -1;
    private string _washerUpgradeLvlString = "washerUpgradeIndex";
    private BuyCanvas _buyCanvas;

    private void Awake()
    {
        InitUpgradeButtons();
        _saveManager.Load();
        _upgradeLvl = _saveManager.SaveData.WasherUpgradeIndex;//PlayerPrefs.GetInt(_washerUpgradeLvlString, 0); //_settings.UpgradeLvl;
        SetUpgradeButtons();
        _washer.SetUpgrades(_upgradeLvl);
    }

    [Inject]
    private void Construct(WasherStorage washer, Wallet wallet, SaveManager saveManager, BuyCanvas buyCanvas)
    {
        _buyCanvas = buyCanvas;
        _washer = washer;
        _wallet = wallet;
        _saveManager = saveManager;
    }

    public void InitUpgradeButtons()
    {
        foreach (var button in _upgradeButtons)
        {
            button.Init(_upgradeButtons.IndexOf(button));
        }
    }

    public void SetUpgradeButtons()
    {
        foreach (var button in _upgradeButtons)
        {
            button.gameObject.SetActive(false);
        }

        if (_upgradeLvl == _washer.Settings.Pause.Length - 1) return;

        _upgradeButtons[_upgradeLvl].gameObject.SetActive(true);
    }

    public void TryUpgrade(int buttonIndex)
    {
        if (_wallet.CheckMoney(_upgradeButtons[buttonIndex].GetPrice()) == false) return;
        _buttonIndex = buttonIndex;
        _buyCanvas.YesCkickEvent += Upgrade;
        _buyCanvas.Show();


    }

    private void Upgrade()
    {
        _buyCanvas.YesCkickEvent -= Upgrade;
        _wallet.RemoveMoney(_upgradeButtons[_buttonIndex].GetPrice());
        _upgradeLvl = ((_upgradeLvl + 1) < _washer.Settings.Pause.Length) ? _upgradeLvl + 1 : _washer.Settings.Pause.Length - 1;
        //PlayerPrefs.SetInt(_washerUpgradeLvlString, _upgradeLvl);

        Debug.LogError($"you upgraded washer to lvl {_upgradeLvl}");
        SetUpgradeButtons();
        _washer.SetUpgrades(_upgradeLvl);
        _buttonIndex = -1;
    }

}
