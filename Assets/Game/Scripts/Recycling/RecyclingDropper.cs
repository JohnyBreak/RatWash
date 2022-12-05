using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingDropper : MonoBehaviour
{
    [SerializeField] private OreSettings.OreType _type;

    private List<Ore> _oreList;
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

    public void SetOreList(List<Ore> oreList)
    {
        _oreList = oreList;
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
        _spawnRoutine = StartCoroutine(SpawnOre());
    }

    private IEnumerator SpawnOre() 
    {
        var wait = new WaitForSeconds(_cooldown);

        foreach (var item in _oreList) 
        {
            item.transform.position = _position;
            item.gameObject.SetActive(true);

            // spawn

           yield return wait;
        }
    }
}
