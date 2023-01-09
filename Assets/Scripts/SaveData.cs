using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public const string TurretsFileName = "turrets.json";
    public const string WallsFileName = "walls.json";
    public const string BarrierFileName = "barriers.json";
    public const string ResearchesFileName = "researches.json";
    public const string ResourcesFileName = "resources.json";
    private void Awake()
    {
        var dir = Directory.CreateDirectory($"{Application.persistentDataPath}\\{LoadedData.LevelNumber}");
        foreach (var file in dir.GetFiles())
        {
            file.Delete();
        }

        var turrets = new Dictionary<string, TurretClass>();
        var barriers = new Dictionary<string, BarrierClass>();
        foreach (var turret in Ship.Turrets)
        {
            turrets[turret.Side.ToString() + turret.PositionOnWall] = turret.TurretClass;
            var barrier = turret.barrierScript;
            barriers[barrier.side.ToString() + barrier.positionOnWall] = barrier.barrierClass;
        }
        SaveObject(turrets, TurretsFileName);
        SaveObject(barriers, BarrierFileName);
        
        var walls = new Dictionary<Side, WallClass>();
        foreach (var wall in Ship.Walls)
        {
            walls[wall.Side] = wall.wallClass;
        }
        SaveObject(walls, WallsFileName);

        var researches = new Dictionary<ResearchType, bool>
        {
            { ResearchType.Turrets, Research.TurretsResearch },
            { ResearchType.TwoTurrets, Research.TwoTurretsResearch },
            { ResearchType.Barrier, Research.BarriersResearch }
        };
        SaveObject(researches, ResearchesFileName);

        var resources = new Dictionary<ResourceType, int>
        {
            { ResourceType.Metal, Resources.Metal.Count },
            { ResourceType.Electronics, Resources.Electronics.Count },
            { ResourceType.Coins, Resources.Coins }
        };
        SaveObject(resources, ResourcesFileName);
    }

    private void SaveObject(object obj, string fileName)
    {
        var json = JsonConvert.SerializeObject(obj);
        File.WriteAllText($"{Application.persistentDataPath}\\{LoadedData.LevelNumber}\\{fileName}", json);
    }
}