using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private bool isFirstLevel;
    [SerializeField] private int levelNumber;
    private bool isCompleted;
    private bool isPreviousCompleted;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(isFirstLevel ? LoadFirstLevel : LoadLevel);
        isCompleted = Directory.Exists($"{Application.persistentDataPath}\\{levelNumber}");
        isPreviousCompleted = Directory.Exists($"{Application.persistentDataPath}\\{levelNumber - 1}");
        button.interactable = isFirstLevel || isCompleted || isPreviousCompleted;
    }

    private void LoadLevel()
    {
        LoadedData.LevelNumber = levelNumber;
        LoadedData.Turrets = GetJson<Dictionary<Tuple<Side, Side>, TurretClass>>(SaveData.TurretsFileName);
        LoadedData.Walls = GetJson<Dictionary<Side, WallClass>>(SaveData.WallsFileName);
        LoadedData.Barriers = GetJson<Dictionary<Tuple<Side, Side>, BarrierClass>>(SaveData.BarrierFileName);
        LoadedData.Researches = GetJson<Dictionary<ResearchType, bool>>(SaveData.ResearchesFileName);
        LoadedData.Resources = GetJson<Dictionary<ResourceType, int>>(SaveData.ResourcesFileName);
        EventAggregator.ShowLevelInfo.Publish();
    }

    private void LoadFirstLevel()
    {
        LoadedData.LevelNumber = levelNumber;
        LoadedData.Turrets = new Dictionary<Tuple<Side, Side>, TurretClass>();
        LoadedData.Walls = new Dictionary<Side, WallClass>();
        LoadedData.Barriers = new Dictionary<Tuple<Side, Side>, BarrierClass>();
        LoadedData.Researches = new Dictionary<ResearchType, bool>
        {
            { ResearchType.Turrets, false },
            { ResearchType.TwoTurrets, false },
            { ResearchType.Barrier, false }
        };
        LoadedData.Resources = new Dictionary<ResourceType, int>
        {
            { ResourceType.Metal, 100 },
            { ResourceType.Electronics, 10 }
        };
        EventAggregator.ShowLevelInfo.Publish();
    }

    private T GetJson<T>(string fileName)
    {
        return JsonConvert.DeserializeObject<T>(
            File.ReadAllText($"{Application.persistentDataPath}/{levelNumber - 1}/{fileName}"));
    }
}
