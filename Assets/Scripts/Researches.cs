public static class Research
{
    private static bool _turretsResearch;
    private static bool _twoTurretsResearch;
    private static bool _barriersResearch;

    public static bool TurretsResearch
    {
        get => _turretsResearch;
        set
        {
            _turretsResearch = value;
            EventAggregator.TurretsResearched.Publish();
        }
    }

    public static bool TwoTurretsResearch
    {
        get => _twoTurretsResearch;
        set
        {
            _twoTurretsResearch = value;
            EventAggregator.TwoTurretsResearched.Publish();
        }
    }

    public static bool BarriersResearch
    {
        get => _barriersResearch;
        set
        {
            _barriersResearch = value;
            EventAggregator.BarriersResearched.Publish();
        }
    }
}
