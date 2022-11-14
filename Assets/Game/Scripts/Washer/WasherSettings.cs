using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WasherSettings", menuName = "ScriptableObjects/WasherSettings", order = 1)]
public class WasherSettings : ScriptableObject
{
    [SerializeField] private float[] _pause;
    [SerializeField] private int[] _maxActiveDropperCount;
    [SerializeField, Min(0)] private int _upgradeLvl;

    public int[] MaxDropperCount => _maxActiveDropperCount;
    public float[] Pause => _pause;
    public int UpgradeLvl => _upgradeLvl;
}
