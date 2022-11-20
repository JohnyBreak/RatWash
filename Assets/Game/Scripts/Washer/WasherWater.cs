using GameOn.TagMaskField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WasherWater : MonoBehaviour
{
    [SerializeField] private TagMask _collectableTag;

    //[Inject] 
    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_collectableTag.Contains(other.gameObject.tag)) return;
        if (!other.gameObject.TryGetComponent<Rat>(out Rat rat)) return;

        _wallet.AddMoney(rat.Settings.Price);
        rat.Wash();
    }
}
