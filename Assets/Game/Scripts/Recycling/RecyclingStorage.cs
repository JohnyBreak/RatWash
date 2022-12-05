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
    private List<Ore> _ratList;
    private List<Ore> _ratListToWash;
    private Dictionary<OreSettings.OreType, int> _ratCountDictionary;
    private float _pause;
    private int _maxActiveDropperCount;
    public RecyclingSettings Settings => _settings;

    private Coroutine _washRoutine;
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

        _ratList = new List<Ore>();
        _ratListToWash = new List<Ore>();

        _saveManager.Load();
        if (_saveManager.SaveData.OreCountDictionary == null)
        {
            _ratCountDictionary = new Dictionary<OreSettings.OreType, int>();
            ResetRatDictionary();
            Debug.LogError("if");
        }
        else
        {
            Debug.LogError("Else");
            _ratCountDictionary = _saveManager.SaveData.OreCountDictionary;
            foreach (var item in _ratCountDictionary)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    AddOre(_preSpawner.GetRat(item.Key));
                }
            }
        }

        OreListChangedEvent?.Invoke();
        RatListChangedForSaveEvent += UpdateRatCountsForSave;
        //_pause = _settings.Pause[_upgradeLvl];
        //_maxActiveDropperCount = _settings.MaxDropperCount[_upgradeLvl];

    }

    private void OnDestroy()
    {
        RatListChangedForSaveEvent -= UpdateRatCountsForSave;
    }

    private void ResetRatDictionary() 
    {
        _ratCountDictionary.Clear();
        foreach (OreSettings.OreType item in Enum.GetValues(typeof(OreSettings.OreType)))
        {
            _ratCountDictionary.Add(item, 0);
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
        if (_ratList.Count < 1) return;
        foreach (var item in _ratList)
        {
            _ratListToWash.Add(item);
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



        _ratList.Clear();

        ResetRatDictionary();

        _saveManager.SaveData.OreCountDictionary = _ratCountDictionary;
        _saveManager.Save();
        OreListChangedEvent?.Invoke();
        if (_washRoutine != null)
        {
            StopCoroutine(_washRoutine);
            _washRoutine = null;
        }
        _washRoutine = StartCoroutine(WashRoutine(_ratListToWash));
    }

    private IEnumerator WashRoutine(List<Ore> washList)
    {
        while (washList.Count > 0)
        {
            yield return null;

            for (int i = 0; i < _maxActiveDropperCount; i++)
            {
                if (washList.Count < 1) break;

                washList[0].transform.position = _droppersList[i].position;
                washList[0].gameObject.SetActive(true);
                washList.Remove(washList[0]);
            }
            yield return new WaitForSeconds(_pause);
        }
    }

    private void ChangeRatPosition(Ore rat)
    {
        rat.transform.position = transform.position + transform.up * 2f;
        rat.gameObject.SetActive(false);
    }

    public void AddOre(Ore rat)
    {
        _ratList.Add(rat);
        OreListChangedEvent?.Invoke();
        RatListChangedForSaveEvent?.Invoke(rat.Settings.Type);
        ChangeRatPosition(rat);
    }

    private void UpdateRatCounts()
    {
        foreach (OreSettings.OreType item in Enum.GetValues(typeof(OreSettings.OreType)))
        {
            _ratCountDictionary[item] = _ratList.Where(x => x.Settings.Type == item).Count();
        }

    }

    private void UpdateRatCountsForSave(OreSettings.OreType type)
    {
        _ratCountDictionary[type] += 1;

        _saveManager.SaveData.OreCountDictionary = _ratCountDictionary;
        _saveManager.Save();
    }

    public Dictionary<OreSettings.OreType, int> GetOreCounts()
    {
        UpdateRatCounts();
        return _ratCountDictionary;
    }

}
