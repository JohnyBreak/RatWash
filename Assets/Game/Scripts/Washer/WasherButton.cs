using GameOn.TagMaskField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WasherButton : MonoBehaviour
{
    [SerializeField] private TagMask _playerTag;

    private WasherStorage _washer;

    [Inject]
    private void Construct(WasherStorage washer)
    {
        _washer = washer;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!_playerTag.Contains(other.gameObject.tag)) return;
        _washer.StartWash();
    }
}
