using System.Collections;
using NTC.Global.Pool;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private OreSettings _settings;
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private Transform _spawnPoint;
    //[SerializeField] 
    private OreButton _button; 

    private Coroutine _spawnRoutine;
    private SpawnerUpgrader _upgrader;
    private RecyclingStorage _washer;
    private bool _activated;

    public OreSettings Settings => _settings;
    public OreButton Button => _button;

    [Inject]
    private void Constructor(RecyclingStorage washer, SpawnerUpgrader upgrader)
    {
        _upgrader = upgrader;
           _washer = washer;
    }

    private void Awake()
    {
        _button = GetComponentInChildren<OreButton>();
    }

    private void Start()
    {
        //CheckActivity();
        StartSpawn();

        //SpawnRat(_settings);
    }

    public void StartSpawn(bool need = false) 
    {
        CheckActivity();

       if(need)_activated = need;

        if (!_activated) return;

        _button.DisableButton();

        _spawnRoutine = StartCoroutine(SpawnRats());
    }

    private void CheckActivity() 
    {
        _activated = _upgrader.CheckSpawnerActive(_settings.Type, _button.GetIndex());
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

    private void SpawnRat(OreSettings ratSettings) 
    {
           Ore rat = NightPool.Spawn(ratSettings.Rat, _spawnPoint.position, Quaternion.identity); //Instantiate(ratSettings.Rat, _spawnPoint.position, Quaternion.identity);

        rat.SetRecycler(_washer);
        //rat.SetSpawner(this);
        rat.SetSettings(ratSettings);
    }

    //public void ReturnRat(Rat rat) 
    //{
    //    rat.transform.position = transform.position;
    //    rat.gameObject.SetActive(true);
    //}

}
