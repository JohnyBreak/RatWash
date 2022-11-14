using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class SpawnerUpgrader : MonoBehaviour
{
    //[SerializeField] private List<WasherUpgradeButton> _upgradeButtons;


    [SerializeField] private List<RatUpgrade> _ratSpawners;

    private WasherStorage _washer;
    private Wallet _wallet;
    private int _upgradeLvl;
    private string _washerUpgradeLvlString = "washerUpgradeIndex";



    private void Awake()
    {
        InitUpgradeButtons();

        _upgradeLvl = PlayerPrefs.GetInt(_washerUpgradeLvlString, 0); //_settings.UpgradeLvl;
        SetUpgradeButtons();
        _washer.SetUpgrades(_upgradeLvl);
    }

    [Inject]
    private void Construct(WasherStorage washer, Wallet wallet)
    {
        _washer = washer;
        _wallet = wallet;
    }

    public void InitUpgradeButtons()
    {
        int tempIndex = 0;
        foreach (var button in _ratSpawners)
        {
            var upgradeIndex = PlayerPrefs.GetInt(button.RatType.ToString(), 0);

            foreach (var spawner in button.Spawners)
            {
                spawner.Button.Init(tempIndex);
                if (tempIndex < upgradeIndex) spawner.gameObject.SetActive(false);

                tempIndex++;
            }
        }
    }

    public void SetUpgradeButtons()
    {
        //foreach (var button in _upgradeButtons)
        //{
        //    button.gameObject.SetActive(false);
        //}

        //if (_upgradeLvl == _washer.Settings.Pause.Length - 1) return;

        //_upgradeButtons[_upgradeLvl].gameObject.SetActive(true);
    }

    public void Upgrade(RatSettings.RatType type)
    {
        /*
        List<RatUpgrade> list = _ratSpawners.Where(l => l.RatType == type).ToList();


        var upgradeIndex = PlayerPrefs.GetInt(type.ToString(), 0);

        if (_wallet.RemoveMoney(list[upgradeIndex].Spawners .GetPrice()) == false) return;


        if ()

        int tempIndex = 0;
        foreach (var button in _ratSpawners)
        {
            var upgradeIndex = PlayerPrefs.GetInt(button.RatType.ToString(), 0);

            foreach (var spawner in button.Spawners)
            {
                if (tempIndex < upgradeIndex) spawner.gameObject.SetActive(false);
            }
        }
        */
        //if (_wallet.RemoveMoney(_upgradeButtons[buttonIndex].GetPrice()) == false) return;

        //_upgradeLvl = ((_upgradeLvl + 1) < _washer.Settings.Pause.Length) ? _upgradeLvl + 1 : _washer.Settings.Pause.Length - 1;
        //PlayerPrefs.SetInt(_washerUpgradeLvlString, _upgradeLvl);
        //Debug.LogError($"you upgraded washer to lvl {_upgradeLvl}");
        //SetUpgradeButtons();
        //_washer.SetUpgrades(_upgradeLvl);
    }

    [System.Serializable]
    public class RatUpgrade
    {
        [SerializeField] private RatSettings.RatType _ratType;
        [SerializeField] private Spawner[] _spawners;
        public RatSettings.RatType RatType =>_ratType;
        public Spawner[] Spawners => _spawners;
    }
}
