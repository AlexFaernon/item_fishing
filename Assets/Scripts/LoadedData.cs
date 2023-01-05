using System;
using System.Collections.Generic;

public static class LoadedData
{
    public static int LevelNumber;
    public static Dictionary<string, TurretClass> Turrets { get; set; } = new();
    public static Dictionary<string, BarrierClass> Barriers { get; set; } = new();
    public static Dictionary<Side, WallClass> Walls { get; set; } = new();
    public static Dictionary<ResearchType, bool> Researches { get; set; } = new();
    public static Dictionary<ResourceType, int> Resources { get; set; } = new();

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