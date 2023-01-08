using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private bool isFirstLevel;
    [SerializeField] private int levelNumber;
    [SerializeField] private int metalCount;
    [SerializeField] private int electronicsCount;
    [SerializeField] private int enemyCounter;
    [SerializeField] private int preparationTime;
    [SerializeField] private int battleTime;
    private bool isCompleted;
    private bool isPreviousCompleted;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowLevelInfo);
        isCompleted = Directory.Exists($"{Application.persistentDataPath}\\{levelNumber}");
        isPreviousCompleted = Directory.Exists($"{Application.persistentDataPath}\\{levelNumber - 1}");
        button.interactable = isFirstLevel || isCompleted || isPreviousCompleted;
    }

    private void ShowLevelInfo()
    {
        LoadedData.LevelNumber = levelNumber;
        LoadedData.MetalCount = metalCount;
        LoadedData.ElectronicsCount = electronicsCount;
        LoadedData.EnemyCounter = enemyCounter;
        LoadedData.PreparationTime = preparationTime;
        LoadedData.BattleTime = battleTime;
        if (isFirstLevel)
        {
            LoadFirstLevel();
        }
        else
        {
            LoadLevel();
        }

        EventAggregator.ShowLevelInfo.Publish();
    }

    public static void ReloadLevel()
    {
        if (LoadedData.LevelNumber == 1)
        {
            LoadFirstLevel();
        }
        else
        {
            LoadLevel();
        }
    }

    private static void LoadLevel()
    {
        LoadedData.Turrets = GetJson<Dictionary<string, TurretClass>>(SaveData.TurretsFileName);
        LoadedData.Walls = GetJson<Dictionary<Side, WallClass>>(SaveData.WallsFileName);
        LoadedData.Barriers = GetJson<Dictionary<string, BarrierClass>>(SaveData.BarrierFileName);
        LoadedData.Researches = GetJson<Dictionary<ResearchType, bool>>(SaveData.ResearchesFileName);
        LoadedData.Resources = GetJson<Dictionary<ResourceType, int>>(SaveData.ResourcesFileName);
    }

    private static void LoadFirstLevel()
    {
        LoadedData.Turrets = new Dictionary<string, TurretClass>();
        LoadedData.Walls = new Dictionary<Side, WallClass>
        {
            { Side.Up, new WallClass { Health = 1 } }
        };
        LoadedData.Barriers = new Dictionary<string, BarrierClass>();
        LoadedData.Researches = new Dictionary<ResearchType, bool>
        {
            { ResearchType.Turrets, false },
            { ResearchType.TwoTurrets, false },
            { ResearchType.Barrier, false }
        };
        LoadedData.Resources = new Dictionary<ResourceType, int>
        {
            { ResourceType.Metal, 0 },
            { ResourceType.Electronics, 0 },
            { ResourceType.Coins, 10 }
        };
    }

    private static T GetJson<T>(string fileName)
    {
        return JsonConvert.DeserializeObject<T>(
            File.ReadAllText($"{Application.persistentDataPath}\\{LoadedData.LevelNumber - 1}\\{fileName}"));
    }
}
