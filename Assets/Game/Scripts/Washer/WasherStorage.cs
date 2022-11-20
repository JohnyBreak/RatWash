using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using System;

public class WasherStorage : MonoBehaviour
{
    public System.Action RatListChangedEvent;
    public System.Action<RatSettings.RatType> RatListChangedForSaveEvent;
    [SerializeField] private WasherSettings _settings;
    [SerializeField] private List<Transform> _droppersList;
    //[SerializeField] private List<WasherDropper> _droppers;

    private SaveManager _saveManager;
    private List<Rat> _ratList;
    private List<Rat> _ratListToWash;
    private Dictionary<RatSettings.RatType, int> _ratCountDictionary;
    private float _pause;
    private int _maxActiveDropperCount;
    public WasherSettings Settings => _settings;

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

        _ratList = new List<Rat>();
        _ratListToWash = new List<Rat>();

        _saveManager.Load();
        if (_saveManager.SaveData.RatCountDictionary == null)
        {
            _ratCountDictionary = new Dictionary<RatSettings.RatType, int>();
            ResetRatDictionary();
            Debug.LogError("if");
        }
        else
        {
            Debug.LogError("Else");
            _ratCountDictionary = _saveManager.SaveData.RatCountDictionary;
            foreach (var item in _ratCountDictionary)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    AddRat(_preSpawner.GetRat(item.Key));
                }
            }
        }

        RatListChangedEvent?.Invoke();
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
        foreach (RatSettings.RatType item in Enum.GetValues(typeof(RatSettings.RatType)))
        {
            _ratCountDictionary.Add(item, 0);
        }

    }

    public void SetUpgrades(int lvl)
    {
        _saveManager.SaveData.WasherUpgradeIndex = lvl;
        _saveManager.Save();
        _pause = _settings.Pause[lvl];
        _maxActiveDropperCount = _settings.MaxDropperCount[lvl];
    }

    public void StartWash()
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

        _saveManager.SaveData.RatCountDictionary = _ratCountDictionary;
        _saveManager.Save();
        RatListChangedEvent?.Invoke();
        if (_washRoutine != null)
        {
            StopCoroutine(_washRoutine);
            _washRoutine = null;
        }
        _washRoutine = StartCoroutine(WashRoutine(_ratListToWash));
    }

    private IEnumerator WashRoutine(List<Rat> washList)
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

    private void ChangeRatPosition(Rat rat)
    {
        rat.transform.position = transform.position + transform.up * 2f;
        rat.gameObject.SetActive(false);
    }

    public void AddRat(Rat rat)
    {
        _ratList.Add(rat);
        RatListChangedEvent?.Invoke();
        RatListChangedForSaveEvent?.Invoke(rat.Settings.Type);
        ChangeRatPosition(rat);
    }

    private void UpdateRatCounts()
    {
        foreach (RatSettings.RatType item in Enum.GetValues(typeof(RatSettings.RatType)))
        {
            _ratCountDictionary[item] = _ratList.Where(x => x.Settings.Type == item).Count();
        }

    }

    private void UpdateRatCountsForSave(RatSettings.RatType type)
    {
        _ratCountDictionary[type] += 1;

        _saveManager.SaveData.RatCountDictionary = _ratCountDictionary;
        _saveManager.Save();
    }

    public Dictionary<RatSettings.RatType, int> GetRatCounts()
    {
        UpdateRatCounts();
        return _ratCountDictionary;
    }

}
