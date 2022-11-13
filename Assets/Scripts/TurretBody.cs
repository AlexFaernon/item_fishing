using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretBody : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Side side;
    public Side Side => side;
    public int Health { get; private set; } = 5;
    public bool isBroken;

    private void Awake()
    {
        Ship.AddTurret(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Enemy")) return;
        
        Health -= 1;
        Debug.Log($"turret hp is {Health}");
        if (Health > 0) return;
        
        isBroken = true;
        transform.parent.GetComponent<Turret>().enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("turret");
    }
}