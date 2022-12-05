using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "OreSettings", menuName = "ScriptableObjects/OreSettings", order = 1)]
public class OreSettings : ScriptableObject
{
    public enum OreType 
    {
        //I  = 0,
		Iron = 0,
        Cooper  = 1,
        Silver  = 2,
        Gold  = 3,

    }

    [SerializeField] private OreType _type;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private Ore _ore;

    public Ore Ore => _ore;
    public string Name => _name;
    public int Price => _price;
    public OreType Type => _type;
}
