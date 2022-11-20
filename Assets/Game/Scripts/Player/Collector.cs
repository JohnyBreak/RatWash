using GameOn.TagMaskField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private LayerMask _ratMask;
    [SerializeField] private float _checkRadius = 1;


    private void Awake()
    {
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    //[SerializeField] private TagMask _collectableTag;
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!_collectableTag.Contains(other.gameObject.tag)) return;
    //    if (!other.gameObject.TryGetComponent<Rat>(out Rat coll)) return;

    //    coll.Collect();
    //}

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Check();
        }
    }

    
    private void Check()
    {
        Collider[] rats = Physics.OverlapSphere(transform.position, _checkRadius, _ratMask);
        if (rats.Length < 1) return;


        foreach (var item in rats)
        {
            item.GetComponent<Rat>().Collect();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }
}
