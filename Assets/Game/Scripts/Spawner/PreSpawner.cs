using NTC.Global.Pool;
using Zenject;
using System.Collections.Generic;
using UnityEngine;

public class PreSpawner : MonoBehaviour
{
    [SerializeField] private List<PreSpawnerData> _ore;
    private RecyclingStorage _recycler;

    [Inject]
    private void Constructor(RecyclingStorage recycler)
    {
        _recycler = recycler;
    }

    public Ore GetOre(OreSettings.OreType type) 
    {
        foreach (var rat in _ore)
        {
            if (rat.Type == type) return SpawnOre(rat.OreSettings);
        }
        return null;
    }

    private Ore SpawnOre(OreSettings ratSettings)
    {
        Ore ore = NightPool.Spawn(ratSettings.Ore);

        ore.SetRecycler(_recycler);
        ore.SetSettings(ratSettings);
        return ore;
    }
}
[System.Serializable]
public class PreSpawnerData 
{
    public OreSettings.OreType Type;
    public OreSettings OreSettings;
}
