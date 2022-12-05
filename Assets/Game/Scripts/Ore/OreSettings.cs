using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "RatSettings", menuName = "ScriptableObjects/RatSettings", order = 1)]
public class OreSettings : ScriptableObject
{
    public enum OreType 
    {
        classic = 0,
        big = 1,
        small = 2,
    }

    [SerializeField] private OreType _type;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private Ore _rat;

    public Ore Rat => _rat;
    public string Name => _name;
    public int Price => _price;
    public OreType Type => _type;
}
