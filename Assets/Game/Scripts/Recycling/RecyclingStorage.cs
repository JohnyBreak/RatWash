using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using System;

public class RecyclingStorage : MonoBehaviour
{
    public System.Action OreListChangedEvent;
    public System.Action<OreSettings.OreType> RatListChangedForSaveEvent;
    [SerializeField] private RecyclingSettings _settings;
    [SerializeField] private List<Transform> _droppersList;
    //[SerializeField] private List<WasherDropper> _droppers;

    private SaveManager _saveManager;
    private List<Ore> _oreList;
    private List<Ore> _oreListToRecycle;
    private Dictionary<OreSettings.OreType, int> _oreCountDictionary;
    private float _pause;
    private int _maxActiveDropperCount;
    public RecyclingSettings Settings => _settings;

    private Coroutine _recycleRoutine;
    private PreSpawner _preSpawner;

    [Inject]
    private void Construct(SaveManager saveManager, PreSpawner preSpawner)
    {
        _preSpawner = preSpawner;
        _saveManager = saveManager;
    }

    private void Awake()
    {
        if (_maxActiveDropperCount >= _droppersList.Count)
            _maxActiveDropperCount = _droppersList.Count;

        _oreList = new List<Ore>();
        _oreListToRecycle = new List<Ore>();

        _saveManager.Load();
        if (_saveManager.SaveData.OreCountDictionary == null)
        {
            _oreCountDictionary = new Dictionary<OreSettings.OreType, int>();
            ResetOreDictionary();
            Debug.LogError("if");
        }
        else
        {
            Debug.LogError("Else");
            _oreCountDictionary = _saveManager.SaveData.OreCountDictionary;
            foreach (var item in _oreCountDictionary)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    AddOre(_preSpawner.GetOre(item.Key));
                }
            }
        }

        OreListChangedEvent?.Invoke();
        RatListChangedForSaveEvent += UpdateOreCountsForSave;
        //_pause = _settings.Pause[_upgradeLvl];
        //_maxActiveDropperCount = _settings.MaxDropperCount[_upgradeLvl];

    }

    private void OnDestroy()
    {
        RatListChangedForSaveEvent -= UpdateOreCountsForSave;
    }

    private void ResetOreDictionary() 
    {
        _oreCountDictionary.Clear();
        foreach (OreSettings.OreType item in Enum.GetValues(typeof(OreSettings.OreType)))
        {
            _oreCountDictionary.Add(item, 0);
        }

    }

    public void SetUpgrades(int lvl)
    {
        _saveManager.SaveData.RecyclingUpgradeIndex = lvl;
        _saveManager.Save();
        _pause = _settings.Pause[lvl];
        _maxActiveDropperCount = _settings.MaxDropperCount[lvl];
    }

    public void StartRecycling()
    {
        if (_oreList.Count < 1) return;
        foreach (var item in _oreList)
        {
            _oreListToRecycle.Add(item);
        }
        /*
                foreach (var item in _droppers)
                {
                    item.StartSpawn();
                }


                    foreach (var item in _ratList)
                    {
                        _ratList.Remove(item);
                    }*/



        _oreList.Clear();

        ResetOreDictionary();

        _saveManager.SaveData.OreCountDictionary = _oreCountDictionary;
        _saveManager.Save();
        OreListChangedEvent?.Invoke();
        if (_recycleRoutine != null)
        {
            StopCoroutine(_recycleRoutine);
            _recycleRoutine = null;
        }
        _recycleRoutine = StartCoroutine(RecycleRoutine(_oreListToRecycle));
    }

    private IEnumerator RecycleRoutine(List<Ore> recycleList)
    {
        while (recycleList.Count > 0)
        {
            yield return null;

            for (int i = 0; i < _maxActiveDropperCount; i++)
            {
                if (recycleList.Count < 1) break;

                recycleList[0].transform.position = _droppersList[i].position;
                recycleList[0].gameObject.SetActive(true);
                recycleList.Remove(recycleList[0]);
            }
            yield return new WaitForSeconds(_pause);
        }
    }

    private void ChangeOrePosition(Ore ore)
    {
        ore.transform.position = transform.position + transform.up * 2f;
        ore.gameObject.SetActive(false);
    }

    public void AddOre(Ore ore)
    {
        _oreList.Add(ore);
        OreListChangedEvent?.Invoke();
        RatListChangedForSaveEvent?.Invoke(ore.Settings.Type);
        ChangeOrePosition(ore);
    }

    private void UpdateOreCounts()
    {
        foreach (OreSettings.OreType item in Enum.GetValues(typeof(OreSettings.OreType)))
        {
            _oreCountDictionary[item] = _oreList.Where(x => x.Settings.Type == item).Count();
        }

    }

    private void UpdateOreCountsForSave(OreSettings.OreType type)
    {
        _oreCountDictionary[type] += 1;

        _saveManager.SaveData.OreCountDictionary = _oreCountDictionary;
        _saveManager.Save();
    }

    public Dictionary<OreSettings.OreType, int> GetOreCounts()
    {
        UpdateOreCounts();
        return _oreCountDictionary;
    }

}
