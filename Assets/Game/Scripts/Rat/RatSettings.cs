using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "RatSettings", menuName = "ScriptableObjects/RatSettings", order = 1)]
public class RatSettings : ScriptableObject
{
    public enum RatType 
    {
        classic = 0,
        big = 1,
        small = 2,
    }

    [SerializeField] private RatType _type;
    [SerializeField] private string _name;
    [SerializeField] private int[] _prices;
    [SerializeField] private Rat _rat;

    public Rat Rat => _rat;
    public string Name => _name;
    public int[] Prices => _prices;
    public RatType Type => _type;
}
