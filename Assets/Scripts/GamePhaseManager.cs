using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhaseManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject win;
    public static bool IsBattlePhase { get; private set; }

    private void Awake()
    {
        Resources.Metal.Count = LoadedData.Resources[ResourceType.Metal];
        Resources.Metal.Count = LoadedData.Resources[ResourceType.Electronics];
        Research.TurretsResearch = LoadedData.Researches[ResearchType.Turrets];
        Research.TwoTurretsResearch = LoadedData.Researches[ResearchType.TwoTurrets];
        Research.BarriersResearch = LoadedData.Researches[ResearchType.Barrier];
        timer.SetTimer(60);
        StartCoroutine(WaitForTimer(OnFishingEnd));
    }

    private void Start()
    {
        Debug.Log(Ship.Walls.Count);
        Debug.Log(Ship.Turrets.Count);
    }

    private IEnumerator WaitForTimer(Action action)
    {
        yield return new WaitUntil(() => timer.isTimeout);
        
        action.Invoke();
    }
    
    private void OnFishingEnd()
    {
        IsBattlePhase = true;
        GameMode.ShouldSwitchToShip = true;
        enemySpawner.enabled = true;
        timer.SetTimer(180);
        StartCoroutine(WaitForTimer(OnBattleEnd));
    }

    private void OnBattleEnd()
    {
        win.SetActive(true);
        Debug.Log("End");
    }

    private void OnDestroy()
    {
        Ship.Walls.Clear();
        Ship.Turrets.Clear();
        PowerUps.UseSecondLife();
        PowerUps.DeactivateTurretBoost();
        Time.timeScale = 1;
    }
}
