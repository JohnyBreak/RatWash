using GameOn.TagMaskField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherWater : MonoBehaviour
{
    [SerializeField] private TagMask _collectableTag;

    private void OnTriggerEnter(Collider other)
    {
        if (!_collectableTag.Contains(other.gameObject.tag)) return;
        if (!other.gameObject.TryGetComponent<Rat>(out Rat rat)) return;

        rat.Wash();
    }
}
