using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhaseManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    public static bool IsBattlePhase { get; private set; }

    private void Awake()
    {
        timer.SetTimer(60);
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
        SwitchMode.ShouldSwitchToShip = true;
        timer.SetTimer(60);
        StartCoroutine(WaitForTimer(OnBattleEnd));
    }

    private void OnBattleEnd()
    {
        Debug.Log("End");
    }
}
