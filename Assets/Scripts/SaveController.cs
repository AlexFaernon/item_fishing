using System;
using System.Collections.Generic;

public static class SaveController
{
    private static Dictionary<Tuple<Side, Side>, TurretClass> _turrets;
    private static Dictionary<Tuple<Side, Side>, Barrier> _barriers;
    private static Dictionary<Side, WallClass> _walls;

    private static Dictionary<ResearchType, bool> _researches
    {
        set
        {
            Research.TurretsResearch = value[ResearchType.Turrets];
            Research.TwoTurretsResearch = value[ResearchType.TwoTurrets];
            Research.BarriersResearch = value[ResearchType.Barrier];
        }
    }
    private static Dictionary<ResourceType, int> _resources;

    public static TurretClass GetSavedTurret(Side side, Side positionOnWall)
    {
        return _turrets.TryGetValue(new Tuple<Side, Side>(side, positionOnWall), out var turret) ? turret : null;
    }

    public static WallClass GetSavedWall(Side side)
    {
        return _walls[side];
    }
}