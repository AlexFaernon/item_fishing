using System;
using System.Collections.Generic;

public static class LoadedData
{
    public static int LevelNumber;
    private static Dictionary<(Side, Side), TurretClass> _turrets = new();
    private static Dictionary<(Side, Side), BarrierClass> _barriers = new();
    private static Dictionary<Side, WallClass> _walls = new();

    private static Dictionary<ResearchType, bool> _researches
    {
        set
        {
            Research.TurretsResearch = value[ResearchType.Turrets];
            Research.TwoTurretsResearch = value[ResearchType.TwoTurrets];
            Research.BarriersResearch = value[ResearchType.Barrier];
        }
    }

    private static Dictionary<ResourceType, int> _resources
    {
        set
        {
            Resources.Metal.Count = value[ResourceType.Metal];
            Resources.Electronics.Count = value[ResourceType.Electronics];
        }
    }

    public static TurretClass GetSavedTurret(Side side, Side positionOnWall)
    {
        return _turrets.TryGetValue((side, positionOnWall), out var turret) ? turret : null;
    }

    public static WallClass GetSavedWall(Side side)
    {
        return _walls.TryGetValue(side, out var wall) ? wall : null;
    }

    public static BarrierClass GetSavedBarrier(Side side, Side positionOnWall)
    {
        return _barriers.TryGetValue((side, positionOnWall), out var barrier) ? barrier : null;
    }
}