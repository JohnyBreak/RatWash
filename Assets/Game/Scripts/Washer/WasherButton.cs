using GameOn.TagMaskField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherButton : MonoBehaviour
{
    [SerializeField] private TagMask _playerTag;

    [SerializeField] private WasherStorage _washer;

    private void OnTriggerEnter(Collider other)
    {
        if (!_playerTag.Contains(other.gameObject.tag)) return;
        _washer.StartWash();
    }
}
