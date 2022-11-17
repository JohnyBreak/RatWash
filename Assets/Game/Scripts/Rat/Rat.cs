using NTC.Global.Pool;
using UnityEngine;

public class Rat : MonoBehaviour
{
    //[SerializeField] 
    private RatSettings _settings;
    private WasherStorage _washer;
    private Spawner _spawner;
    private Rigidbody _rb;
    public RatSettings Settings => _settings;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    public void Collect()
    {
        _rb.velocity = Vector3.zero;
        _washer.AddRat(this);
        //gameObject.SetActive(false);
    }

    public void Wash() 
    {
        _rb.velocity = Vector3.zero;
        NightPool.Despawn(this);
        //Destroy(gameObject);
        //gameObject.SetActive(false);
        //_spawner.ReturnRat(this);
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
