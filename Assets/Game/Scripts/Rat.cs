using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    //[SerializeField] 
    private RatSettings _settings;
    private WasherStorage _washer;
    private Spawner _spawner;
    public RatSettings Settings => _settings;

    public void Collect() 
    {
        _washer.AddRat(this);
        //gameObject.SetActive(false);
    }
    public void Wash() 
    {
        gameObject.SetActive(false);
        _spawner.ReturnRat(this);
    }

    public void SetSettings(RatSettings settings)
    {
        _settings = settings;
    }

    public void SetWasher(WasherStorage washer) 
    {
        _washer = washer;
    }
    public void SetSpawner(Spawner spawner) 
    {
        _spawner = spawner;
    }
}
