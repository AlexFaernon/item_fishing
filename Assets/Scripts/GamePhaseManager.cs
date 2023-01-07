using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhaseManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject battleBegin;
    public static bool IsBattlePhase { get; private set; }

    private void Awake()
    {
        IsBattlePhase = false;
        timer.SetTimer(LoadedData.PreparationTime);
        StartCoroutine(WaitForTimer(OnFishingEnd));
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
        StartCoroutine(PrepareForBattle());
    }

    private IEnumerator PrepareForBattle()
    {
        Time.timeScale = 0;
        battleBegin.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        battleBegin.SetActive(false);
        Time.timeScale = 1;
        enemySpawner.enabled = true;
        timer.SetTimer(LoadedData.BattleTime);
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
