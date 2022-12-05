[System.Serializable]
public class SpawnerData
{
    public OreSettings.OreType RatType;
    public int Index;
    public SpawnerData(OreSettings.OreType type, int index) 
    {
        RatType = type;
        Index = index;
    }
}