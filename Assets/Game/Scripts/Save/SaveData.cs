using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int MoneyAmount;
    public int RecyclingUpgradeIndex;
    //public List<Rat> RatList;
    public List<SpawnerData> SpawnerDataList;
    public Dictionary<OreSettings.OreType, int> OreCountDictionary;
}
