using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonCanvasEnabler : MonoBehaviour
{
    private GameObject _canvasObject;
    [SerializeField] private float _checkRadius = 1;
    [SerializeField]  private LayerMask _playerMask;
    private bool _switch = true;
    private bool run = true;

    private void Awake()
    {
        //SphereCollider sc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        //sc.radius = _checkRadius;
        //sc.isTrigger = true;
        _canvasObject = GetComponentInChildren<Canvas>().gameObject;

        GetComponentInChildren<TextMeshProUGUI>().text = GetComponentInParent<IGroundButton>().GetPrice().ToString();

        _canvasObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        StartCoroutine(ChekRoutine());
    }

    private IEnumerator ChekRoutine() 
    {
        var wait = new WaitForSeconds(0.2f);
        while (run)
        {
            Check();

            yield return wait;
        }
    }

    private void Check() 
    {
        if (Physics.CheckSphere(transform.position, _checkRadius, _playerMask))
        {
            if (_switch)
            {
                _switch = false;

                _canvasObject.SetActive(true);
            }
        }
        else
        {
            if (!_switch)
            {
                _switch = true;

                _canvasObject.SetActive(false);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    _canvasObject.SetActive(false);
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }
}
