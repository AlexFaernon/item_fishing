using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToFishing : MonoBehaviour
{
    [SerializeField] private GameObject helper;
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
        if (IsPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SwitchMode.Mode = Mode.Ship;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col is BoxCollider2D && col.CompareTag("Player"))
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
