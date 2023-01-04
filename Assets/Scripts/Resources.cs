public static class Resources
{
    public static int Coins;
    public static class Metal
    {
        public static int Count = 100;

        public const int MaxTimeToCatch = 7;
    }
    
    public static class Electronics
    {
        public static int Count = 10;
        public const int MaxTimeToCatch = 15;
    }
}

public enum ResourceType
{
    Metal,
    Electronics,
    Coins
}