using NTC.Global.Pool;
using Zenject;
using System.Collections.Generic;
using UnityEngine;

public class PreSpawner : MonoBehaviour
{
    [SerializeField] private List<PreSpawnerData> _rats;
    private RecyclingStorage _washer;

    [Inject]
    private void Constructor(RecyclingStorage washer)
    {
        _washer = washer;
    }

    public Ore GetOre(OreSettings.OreType type) 
    {
        foreach (var rat in _rats)
        {
            if (rat.Type == type) return SpawnRat(rat.RatSettings);
        }
        return null;
    }

    private Ore SpawnRat(OreSettings ratSettings)
    {
        Ore rat = NightPool.Spawn(ratSettings.Ore);

        rat.SetRecycler(_washer);
        rat.SetSettings(ratSettings);
        return rat;
    }
}
[System.Serializable]
public class PreSpawnerData 
{
    public OreSettings.OreType Type;
    public OreSettings RatSettings;
}
