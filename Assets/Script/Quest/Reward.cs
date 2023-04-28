[System.Serializable]
public class Reward
{
    public enum RewardType
    {
        Gold,
        Experience,
        Item,
        Special
    }

    public RewardType type;
    public int amount;
    public string description;
}