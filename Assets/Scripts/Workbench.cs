using System;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    [SerializeField] private GameObject eHelper;
    [SerializeField] private GameObject qHelper;
    [SerializeField] private GameObject upgrades;
    [SerializeField] private GameObject research;
    private bool isPlayerInRange;

    private bool IsPlayerInRange
    {
        get => isPlayerInRange;
        set
        {
            isPlayerInRange = value; 
            eHelper.SetActive(value);
            qHelper.SetActive(value);
        }
    }

    private void Update()
    {
        if (GamePhaseManager.IsBattlePhase || !IsPlayerInRange)
        {
            qHelper.SetActive(false);
            eHelper.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && upgrades.activeSelf == false && research.activeSelf == false)
        {
            upgrades.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameMode.Mode = Mode.Ship;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col is BoxCollider2D && col.CompareTag("Player") && !GamePhaseManager.IsBattlePhase)
        {
            IsPlayerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col is BoxCollider2D && col.CompareTag("Player"))
        {
            IsPlayerInRange = false;
        }
    }
}