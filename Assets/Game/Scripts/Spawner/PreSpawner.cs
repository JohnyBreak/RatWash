using NTC.Global.Pool;
using Zenject;
using System.Collections.Generic;
using UnityEngine;

public class PreSpawner : MonoBehaviour
{
    [SerializeField] private List<PreSpawnerData> _rats;
    private WasherStorage _washer;

    [Inject]
    private void Constructor(WasherStorage washer)
    {
        _washer = washer;
    }

    public Rat GetRat(RatSettings.RatType type) 
    {
        foreach (var rat in _rats)
        {
            if (rat.Type == type) return SpawnRat(rat.RatSettings);
        }
        return null;
    }

    private Rat SpawnRat(RatSettings ratSettings)
    {
        Rat rat = NightPool.Spawn(ratSettings.Rat);

        rat.SetWasher(_washer);
        rat.SetSettings(ratSettings);
        return rat;
    }
}
[System.Serializable]
public class PreSpawnerData 
{
    public RatSettings.RatType Type;
    public RatSettings RatSettings;
}
