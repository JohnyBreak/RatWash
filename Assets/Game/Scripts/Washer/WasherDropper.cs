using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherDropper : MonoBehaviour
{
    [SerializeField] private RatSettings.RatType _type;

    private List<Rat> _ratList;
    private Coroutine _spawnRoutine;
    private float _cooldown;
    private Vector3 _position;

    void Start()
    {
        _position = transform.position;
    }

    void Update()
    {

    }

    public void SetRatList(List<Rat> ratList)
    {
        _ratList = ratList;
    }

    public void SetCooldown(float cooldown)
    {
        _cooldown = cooldown;
    }

    public void StartSpawn() 
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
        _spawnRoutine = StartCoroutine(SpawnRats());
    }

    private IEnumerator SpawnRats() 
    {
        var wait = new WaitForSeconds(_cooldown);

        foreach (var item in _ratList) 
        {
            item.transform.position = _position;
            item.gameObject.SetActive(true);

            // spawn

           yield return wait;
        }
    }
}
