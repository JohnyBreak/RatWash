using GameOn.TagMaskField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RatButton : MonoBehaviour, IGroundButton
{
    [SerializeField] private TagMask _playerTag;

    [SerializeField, Min(0)] private int _upgradePrice = 0;
    //[SerializeField, Min(0)] private int _index = 0;
    private Spawner _spawner;

    [SerializeField] private int _index;

    private SpawnerUpgrader _spawnerUpgrader;

    public int GetPrice()
    {
        return _upgradePrice;
    }
    public int GetIndex()
    {
        return _index;
    }

    private void Awake()
    {
        _spawner = GetComponentInParent<Spawner>();

    }

    [Inject]
    private void Construct(SpawnerUpgrader upgrader)
    {
        _spawnerUpgrader = upgrader;
    }

    public void Init(int index)
    {
        _index = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        HandlePressButton(other);
    }

    public void HandlePressButton(Collider other)
    {
        if (!_playerTag.Contains(other.gameObject.tag)) return;
        _spawnerUpgrader.Upgrade(_spawner);
    }

    public void DisableButton()
    {
        gameObject.SetActive(false);
    }
}
