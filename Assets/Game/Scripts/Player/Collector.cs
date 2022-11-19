using GameOn.TagMaskField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
   // [SerializeField] private LayerMask _playerMask;
    //[SerializeField] private float _checkRadius = 1;
    

    [SerializeField] private TagMask _collectableTag;
    private void OnTriggerEnter(Collider other)
    {
        if (!_collectableTag.Contains(other.gameObject.tag)) return;
        if (!other.gameObject.TryGetComponent<Rat>(out Rat coll)) return;
        
        coll.Collect();
    }
    /*
    private void Check()
    {
        if (Physics.CheckSphere(transform.position, _checkRadius, _playerMask))
        { 
        }
    }*/
}
