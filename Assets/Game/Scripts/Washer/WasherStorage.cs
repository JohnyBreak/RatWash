using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherStorage : MonoBehaviour
{
    [SerializeField] private WasherSettings _settings;
    [SerializeField] private List<Transform> _droppersList;
    //[SerializeField] private List<WasherDropper> _droppers;

    private List<Rat> _ratList;
    private List<Rat> _ratListToWash;

    private float _pause;
    private int _maxActiveDropperCount;
    public WasherSettings Settings => _settings;

    private Coroutine _washRoutine;

    private void Awake()
    {
        if (_maxActiveDropperCount >= _droppersList.Count)
            _maxActiveDropperCount = _droppersList.Count;

        //_pause = _settings.Pause[_upgradeLvl];
        //_maxActiveDropperCount = _settings.MaxDropperCount[_upgradeLvl];

        _ratList = new List<Rat>();
        _ratListToWash = new List<Rat>();
    }

    public void SetUpgrades(int lvl) 
    {
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
        ChangeRatPosition(rat);
    }

}
