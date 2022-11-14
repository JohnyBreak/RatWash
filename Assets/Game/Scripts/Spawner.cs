using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //[SerializeField] 
    private WasherStorage _washer;
    [SerializeField] private RatSettings _settings;

    private void Awake()
    {
        _washer = FindObjectOfType<WasherStorage>();
    }

    private void Start()
    {
        SpawnRat(_settings);
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
