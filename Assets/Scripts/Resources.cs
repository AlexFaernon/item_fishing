public static class Resources
{
    public static class Metal
    {
        private static int _count = 99;

        public static int Count
        {
            get => _count;
            set
            {
                _count = value;
                EventAggregator.MetalUpdate.Publish(value);
            }
        }
        public const int MaxTimeToCatch = 7;
    }
    
    public static class Electronics
    {
        public static int Count;
        public const int MaxTimeToCatch = 15;
    }
}