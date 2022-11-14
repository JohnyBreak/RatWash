using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WasherUpgrader : MonoBehaviour
{
    [SerializeField] private List<WasherUpgradeButton> _upgradeButtons;
    private WasherStorage _washer;
    private Wallet _wallet;
    private int _upgradeLvl;
    private string _washerUpgradeLvlString = "washerUpgradeIndex";

    private void Awake()
    {
        InitUpgradeButton();

        _upgradeLvl = PlayerPrefs.GetInt(_washerUpgradeLvlString, 0); //_settings.UpgradeLvl;
        SetUpgradeButton();
        _washer.SetUpgrades(_upgradeLvl);
    }

    [Inject]
    private void Construct(WasherStorage washer, Wallet wallet)
    {
        _washer = washer;
        _wallet = wallet;
    }

    private void InitUpgradeButton()
    {
        foreach (var button in _upgradeButtons)
        {
            button.Init(_upgradeButtons.IndexOf(button));
        }
    }

    private void SetUpgradeButton()
    {
        foreach (var button in _upgradeButtons)
        {
            button.gameObject.SetActive(false);
        }

        if (_upgradeLvl == _washer.Settings.Pause.Length - 1) return;

        _upgradeButtons[_upgradeLvl].gameObject.SetActive(true);
    }

    public void UpgradeWasher(int buttonIndex)
    {
        if (_wallet.RemoveMoney(_upgradeButtons[buttonIndex].GetPrice()) == false) return;

        _upgradeLvl = ((_upgradeLvl + 1) < _washer.Settings.Pause.Length) ? _upgradeLvl + 1 : _washer.Settings.Pause.Length - 1;
        PlayerPrefs.SetInt(_washerUpgradeLvlString, _upgradeLvl);
        Debug.LogError($"you upgraded washer to lvl {_upgradeLvl}");
        SetUpgradeButton();
        _washer.SetUpgrades(_upgradeLvl);
    }

}
