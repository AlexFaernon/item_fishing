using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer: MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private float initialTime;
    
    private float timeLeft;

    [NonSerialized] public bool isTimeout;

    public void SetTimer(int seconds)
    {
        isTimeout = false;
        timeLeft = initialTime = seconds;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (GamePhaseManager.IsBattlePhase && initialTime - timeLeft <= 60)
            {
                PowerUps.DeactivateTurretBoost();
            }
            UpdateTimeText();
            yield return null;
        }

        if (GamePhaseManager.IsBattlePhase)
        {
            PowerUps.DeactivateTurretBoost();
        }
        isTimeout = true;
        Time.timeScale = 1;
        Debug.Log("Stop timer");
    }

    private void UpdateTimeText()
    {
        if (timeLeft < 0)
            timeLeft = 0;
 
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}