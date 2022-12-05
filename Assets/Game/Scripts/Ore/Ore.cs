using NTC.Global.Pool;
using UnityEngine;

public class Ore : MonoBehaviour
{
    //[SerializeField] 
    private OreSettings _settings;
    private RecyclingStorage _recycler;
    //private Spawner _spawner;
    private Rigidbody _rb;
    public OreSettings Settings => _settings;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    public void Collect()
    {
        _rb.velocity = Vector3.zero;
        _recycler.AddOre(this);
        //gameObject.SetActive(false);
    }

    public void Recycle() 
    {
        _rb.velocity = Vector3.zero;
        NightPool.Despawn(this);
        //Destroy(gameObject);
        //gameObject.SetActive(false);
        //_spawner.ReturnRat(this);
    }

    public void SetSettings(OreSettings settings)
    {
        _settings = settings;
    }

    public void SetRecycler(RecyclingStorage recycler) 
    {
        _recycler = recycler;
    }
    //public void SetSpawner(Spawner spawner) 
    //{
    //    _spawner = spawner;
    //}
}
