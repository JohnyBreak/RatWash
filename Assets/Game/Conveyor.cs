using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private List<OreSettings.OreType> _types;
    private Rigidbody _rb;
    private SaveManager _saveManager;
    private bool _active = false;
    private MeshRenderer _mr;
    private SpawnerUpgrader _upgrader;

    [Inject]
    private void Construct(SaveManager saveManager, SpawnerUpgrader upgrader)
    {
        _saveManager = saveManager;
        _upgrader = upgrader;
    }

    void Start()
    {
        _upgrader.SpawnerUpgradeEvent += Init;

        _rb = GetComponent<Rigidbody>();
        _mr = GetComponent<MeshRenderer>();
        _mr.enabled = false;
        Init();
    }

    void FixedUpdate()
    {
        if (!_active) return;

        Vector3 pos = _rb.position;
        _rb.position -= transform.forward * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(pos);
    }

    public void Init()
    {
        if (_saveManager.SaveData.SpawnerDataList == null) return;

        foreach (var item in _saveManager.SaveData.SpawnerDataList)
        {
            foreach (var type in _types)
            {
                if (item.OreType == type) 
                {
                    //activate
                    Activate();
                    return;
                }
            }
        } 
    }

    private void Activate() 
    {
        _mr.enabled = true;
        _active = true;
    }

    private void OnDestroy()
    {
        _upgrader.SpawnerUpgradeEvent -= Init;
    }
}
