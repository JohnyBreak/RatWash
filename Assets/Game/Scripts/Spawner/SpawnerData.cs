[System.Serializable]
public class SpawnerData
{
    public OreSettings.OreType OreType;
    public int Index;
    public SpawnerData(OreSettings.OreType type, int index) 
    {
        OreType = type;
        Index = index;
    }
}