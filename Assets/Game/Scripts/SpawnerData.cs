[System.Serializable]
public class SpawnerData
{
    public RatSettings.RatType RatType;
    public int Index;
    public SpawnerData(RatSettings.RatType type, int index) 
    {
        RatType = type;
        Index = index;
    }
}