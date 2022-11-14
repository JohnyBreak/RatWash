using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private RatSettings _settings;
    [SerializeField] private float _cooldown = 2f;
    private Coroutine _spawnRoutine;

    private WasherStorage _washer;

    [Inject]
    private void Constructor(WasherStorage washer)
    {
        _washer = washer;
    }

    private void Start()
    {
        _spawnRoutine = StartCoroutine(SpawnRats());
        //SpawnRat(_settings);
    }

    private IEnumerator SpawnRats() 
    {
        var wait = new WaitForSeconds(_cooldown);

        while (true) 
        {
            yield return null;

            SpawnRat(_settings);


            yield return wait;
        }
    }

    private void SpawnRat(RatSettings ratSettings) 
    {
        Rat rat = Instantiate(ratSettings.Rat, transform.position, Quaternion.identity);

        rat.SetWasher(_washer);
        rat.SetSpawner(this);
        rat.SetSettings(ratSettings);
    }

    public void ReturnRat(Rat rat) 
    {
        rat.transform.position = transform.position;
        rat.gameObject.SetActive(true);
    }

}
