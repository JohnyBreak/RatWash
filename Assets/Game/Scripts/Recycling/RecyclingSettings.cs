using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecyclingSettings", menuName = "ScriptableObjects/RecyclingSettings", order = 1)]
public class RecyclingSettings : ScriptableObject
{
    [SerializeField] private float[] _pause;
    [SerializeField] private int[] _maxActiveDropperCount;
    //[SerializeField, Min(0)] private int _upgradeLvl;

    public int[] MaxDropperCount => _maxActiveDropperCount;
    public float[] Pause => _pause;
    //public int UpgradeLvl => _upgradeLvl;
}
