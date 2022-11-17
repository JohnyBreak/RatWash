using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SpawnerUpgrader : MonoBehaviour
{
    //[SerializeField] private List<WasherUpgradeButton> _upgradeButtons;


    [SerializeField] private List<RatUpgrade> _ratSpawners;
    private Dictionary<RatSettings.RatType, int> _spawnerButtonsUpgradeDictionary;
    private Wallet _wallet;
    private SaveManager _saveManager;


    private void Awake()
    {
        //Debug.LogError("PlayerPrefs");
        if (_saveManager.SaveData.SpawnerButtonsUpgradeDictionary == null)
        {
            //RatSettings.RatType status;

            _saveManager.SaveData.SpawnerButtonsUpgradeDictionary = new Dictionary<RatSettings.RatType, int>();
            //foreach (var item in Enum.GetNames(typeof(RatSettings.RatType)))
            //{
            //    Enum.TryParse(item, out status);
            //    _saveManager.SaveData.SpawnerButtonsUpgradeDictionary.Add(status, 0);
            //}
            //_saveManager.Save();
        }
        _spawnerButtonsUpgradeDictionary = _saveManager.SaveData.SpawnerButtonsUpgradeDictionary;
        //InitUpgradeButtons();


        //_upgradeLvl = PlayerPrefs.GetInt(_washerUpgradeLvlString, 0); //_settings.UpgradeLvl;
        //SetUpgradeButtons();
        // _washer.SetUpgrades(_upgradeLvl);
    }

    [Inject]
    private void Construct(Wallet wallet, SaveManager saveManager)
    {
        _wallet = wallet;
        _saveManager = saveManager;
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

    public void Upgrade(Spawner spawner, int index)
    {
        if (_wallet.RemoveMoney(spawner.Button.GetPrice()) == false) return;

        Debug.LogError("PlayerPrefs");
        //PlayerPrefs.SetInt(spawner.Settings.Type.ToString() + spawner.Button.GetIndex(), 1);

        _spawnerButtonsUpgradeDictionary.Add(spawner.Settings.Type, index);
        _saveManager.SaveData.SpawnerButtonsUpgradeDictionary = _spawnerButtonsUpgradeDictionary;
        _saveManager.Save();
        spawner.StartSpawn(true);
    }

    public bool CheckSpawnerActive(RatSettings.RatType type, int index)
    {
        var tempListByType = _spawnerButtonsUpgradeDictionary.Where(x => x.Key == type && x.Value == index);
        if (tempListByType == null) 
        {
            return true;
        }

        return false;

        //var temp = _saveManager.SaveData.SpawnerButtonsUpgradeDictionary.Where(x => x.Key == type).First();
        //Debug.LogError(temp.Key + " " + temp.Value);

        //return temp.Value; //PlayerPrefs.GetInt(type.ToString() + index, 0);
    }

    [System.Serializable]
    public class RatUpgrade
    {
        [SerializeField] private RatSettings.RatType _ratType;
        [SerializeField] private Spawner[] _spawners;
        public RatSettings.RatType RatType => _ratType;
        public Spawner[] Spawners => _spawners;
    }
}
