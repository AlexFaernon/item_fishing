using System;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    [SerializeField] private GameObject helper;
    [SerializeField] private GameObject upgrades;
    [SerializeField] private GameObject research;
    private bool isPlayerInRange;

    private bool IsPlayerInRange
    {
        get => isPlayerInRange;
        set
        {
            isPlayerInRange = value; 
            helper.SetActive(value);
        }
    }

    private void Update()
    {
        if (IsPlayerInRange && Input.GetKeyDown(KeyCode.E) && upgrades.activeSelf == false &&
            research.activeSelf == false)
        {
            upgrades.SetActive(true);
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