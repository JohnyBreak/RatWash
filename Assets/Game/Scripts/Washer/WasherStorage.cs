using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherStorage : MonoBehaviour
{
    [SerializeField] private List<Transform> _droppersList;
    [SerializeField] private int _maxActiveDropperCount;
    [SerializeField] private List<Rat> _ratList;
    [SerializeField] private List<Rat> _ratListToWash;
    [SerializeField] private float _pause;

    [SerializeField] private List<WasherDropper> _droppers; 

    //private Coroutine _washRoutine;

    private void Awake()
    {
        if (_maxActiveDropperCount >= _droppersList.Count)
            _maxActiveDropperCount = _droppersList.Count;

        _ratList = new List<Rat>();
        _ratListToWash = new List<Rat>();
    }

    public void StartWash()
    {
        if (_ratList.Count < 1) return;
        foreach (var item in _ratList)
        {
            _ratListToWash.Add(item);
        }

        foreach (var item in _droppers)
        {
            item.StartSpawn();
        }

        /*
            foreach (var item in _ratList)
            {
                _ratList.Remove(item);
            }
        */


       /* _ratList.Clear();
        if (_washRoutine != null)
        {
            StopCoroutine(_washRoutine);
            _washRoutine = null;
        }
        _washRoutine = StartCoroutine(WashRoutine(_ratListToWash));*/
    }

    /*private IEnumerator WashRoutine(List<Rat> washList) 
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

    private void ContinueWash() 
    {
        StartCoroutine(WashRoutine(_ratListToWash));
    }*/

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
