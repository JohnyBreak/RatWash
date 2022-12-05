using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SpawnerUpgrader : MonoBehaviour
{
    //[SerializeField] private List<WasherUpgradeButton> _upgradeButtons;


    [SerializeField] private List<RatUpgrade> _ratSpawners;
    private List<SpawnerData> _spawnerDataList;
    private Wallet _wallet;
    private SaveManager _saveManager;
    private BuyCanvas _buyCanvas;
    private Spawner _spawner;
    private int _buttonIndex;

    [Inject]
    private void Construct(Wallet wallet, SaveManager saveManager, BuyCanvas buyCanvas)
    {
        _buyCanvas = buyCanvas;
        _wallet = wallet;
        _saveManager = saveManager;
    }

    private void Awake()
    {
        if (_saveManager.SaveData.SpawnerDataList == null)
        {
            _saveManager.SaveData.SpawnerDataList = new List<SpawnerData>();

        }
        _spawnerDataList = _saveManager.SaveData.SpawnerDataList;
    }

   

    //public void InitUpgradeButtons()
    //{
    //    //int tempIndex = 0;
    //    //Dictionary<RatSettings.RatType, int> temp = new Dictionary<RatSettings.RatType, int>();
    //    foreach (var type in _ratSpawners)
    //    {

    //        var tempListByType = _spawnerButtonsUpgradeDictionary.Where(x => x.Key == type.RatType).ToList();

    //        foreach (var item in tempListByType)
    //        {
    //            foreach (var spawner in type.Spawners)
    //            {
    //                if (spawner.Button.GetIndex() == item.Value) 
    //                {
    //                    spawner.gameObject.SetActive(false);
    //                    break;
    //                }
    //            }
    //        }


    //        //if (_spawnerButtonsUpgradeDictionary.ContainsKey(type.RatType) == false)
    //        //{
    //        //    _spawnerButtonsUpgradeDictionary.Add(type.RatType, 0);


    //        //}


    //        //Debug.LogError("PlayerPrefs");
    //        //var upgradeIndex = PlayerPrefs.GetInt(type.RatType.ToString(), 0);

    //        //foreach (var spawner in type.Spawners)
    //        //{
    //        //    if(_spawnerButtonsUpgradeDictionary[type.RatType] == null)
    //        //    spawner.Button.Init(tempIndex);
    //        //    if (tempIndex < upgradeIndex) spawner.gameObject.SetActive(false);

    //        //    tempIndex++;
    //        //}
    //    }
    //}

    //public void SetUpgradeButtons()
    //{
    //    //foreach (var button in _upgradeButtons)
    //    //{
    //    //    button.gameObject.SetActive(false);
    //    //}

    //    //if (_upgradeLvl == _washer.Settings.Pause.Length - 1) return;

    //    //_upgradeButtons[_upgradeLvl].gameObject.SetActive(true);
    //}

    public void TryUpgrade(Spawner spawner, int index)
    {
        if (_wallet.CheckMoney(spawner.Button.GetPrice()) == false) return;

        _spawner = spawner;
        _buttonIndex = index;
        _buyCanvas.YesCkickEvent += Upgrade;
        _buyCanvas.NoCkickEvent += DeclineUpgrade;
        _buyCanvas.Show(spawner.Button.GetPrice());
    }

    private void Upgrade()
    {
        _wallet.RemoveMoney(_spawner.Button.GetPrice());

        _spawnerDataList.Add(new SpawnerData(_spawner.Settings.Type, _buttonIndex));
        _saveManager.SaveData.SpawnerDataList = _spawnerDataList;
        _saveManager.Save();

        _spawner.StartSpawn(true);
        DeclineUpgrade();
    }

    private void DeclineUpgrade()
    {
        _buyCanvas.NoCkickEvent -= DeclineUpgrade;

        _buyCanvas.YesCkickEvent -= Upgrade;
        _spawner = null;
        _buttonIndex = -1;
    }

    public bool CheckSpawnerActive(OreSettings.OreType type, int index)
    {
        foreach (var spawner in _spawnerDataList)
        {
            if (spawner.RatType == type && spawner.Index == index)
            {
                return true;
            }
        }
        return false;
    }

    [System.Serializable]
    public class RatUpgrade
    {
        [SerializeField] private OreSettings.OreType _ratType;
        [SerializeField] private Spawner[] _spawners;
        public OreSettings.OreType RatType => _ratType;
        public Spawner[] Spawners => _spawners;
    }
}
