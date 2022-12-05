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
    private RecyclingStorage _storage;
    private bool _activated;

    public OreSettings Settings => _settings;
    public OreButton Button => _button;

    [Inject]
    private void Constructor(RecyclingStorage storage, SpawnerUpgrader upgrader)
    {
        _upgrader = upgrader;
           _storage = storage;
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

        _spawnRoutine = StartCoroutine(SpawnOreRoutine());
    }

    private void CheckActivity() 
    {
        _activated = _upgrader.CheckSpawnerActive(_settings.Type, _button.GetIndex());
        _spawnPoint.gameObject.SetActive(_activated);
    }

    private IEnumerator SpawnOreRoutine()
    {
        var wait = new WaitForSeconds(_cooldown);

        while (true) 
        {
            yield return null;

            SpawnOre(_settings);

            yield return wait;
        }
    }

    private void SpawnOre(OreSettings ratSettings) 
    {
           Ore ore = NightPool.Spawn(ratSettings.Ore, _spawnPoint.position, Quaternion.identity); //Instantiate(ratSettings.Rat, _spawnPoint.position, Quaternion.identity);

        ore.SetRecycler(_storage);
        ore.gameObject.layer = LayerMask.NameToLayer("Ore");
        //rat.SetSpawner(this);
        ore.SetSettings(ratSettings);
    }

    //public void ReturnRat(Rat rat) 
    //{
    //    rat.transform.position = transform.position;
    //    rat.gameObject.SetActive(true);
    //}

}
