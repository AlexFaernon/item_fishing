using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Research
{
    private static bool _turretsResearch;
    public static bool TwoTurretsResearch;
    public static bool BarriersResearch;

    public static bool TurretsResearch
    {
        get => _turretsResearch;
        set
        {
            _turretsResearch = value;
            EventAggregator.TurretsResearched.Publish();
        }
    }
}
