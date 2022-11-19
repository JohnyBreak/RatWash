using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerButtonCanvas : MonoBehaviour
{
    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(_cameraTransform);
        transform.Rotate(0, 180, 0);
    }
}
