﻿using System.Collections.Generic;

public static class LoadedData
{
    public static int LevelNumber;
    public static int MetalCount = 5;
    public static int ElectronicsCount = 2;
    public static int EnemyCounter = 5;
    public static int PreparationTime = 60;
    public static int BattleTime = 60;
    public static Dictionary<string, TurretClass> Turrets { get; set; } = new();
    public static Dictionary<string, BarrierClass> Barriers { get; set; } = new();
    public static Dictionary<Side, WallClass> Walls { get; set; } = new();

    public static Dictionary<ResearchType, bool> Researches
    {
        set
        {
            Research.TurretsResearch = value[ResearchType.Turrets];
            Research.TwoTurretsResearch = value[ResearchType.TwoTurrets];
            Research.BarriersResearch = value[ResearchType.Barrier];
        }
    }

    public static Dictionary<ResourceType, int> Resources
    {
        set
        {
            global::Resources.Metal.Count = value[ResourceType.Metal];
            global::Resources.Electronics.Count = value[ResourceType.Electronics];
            global::Resources.Coins = value[ResourceType.Coins];
        }
    }

    public static TurretClass GetSavedTurret(Side side, Side positionOnWall)
    {
        return Turrets.TryGetValue(side.ToString() + positionOnWall, out var turret) ? turret : null;
    }

    public static WallClass GetSavedWall(Side side)
    {
        return Walls.TryGetValue(side, out var wall) ? wall : null;
    }

    public static BarrierClass GetSavedBarrier(Side side, Side positionOnWall)
    {
        return Barriers.TryGetValue(side.ToString() + positionOnWall, out var barrier) ? barrier : null;
    }
}