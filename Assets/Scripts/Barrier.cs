using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Barrier : MonoBehaviour
{
    public BarrierClass barrierClass;
    public Side side;
    public Side positionOnWall;
    public int CooldownRank
    {
        get => barrierClass.CooldownRank;
        set => barrierClass.CooldownRank = value;
    }
    public int CooldownMaxRank => new[] { 25, 20, 18, 15 }.Length - 1;
    private int Cooldown => new[] { 25, 20, 18, 15 }[CooldownRank];
    public int NextUpgradeCost => new[] { 10, 20, 30, 0 }[CooldownRank];
    public bool IsReady => !(isActive || isOnCooldown);
    private bool isActive;
    private bool IsActive
    {
        set
        {
            isActive = value;
            spriteRenderer.color = value ? Color.white : new Color(1,1,1, 0.5f);
            collider2D.enabled = value;
        }
    }

    private bool isOnCooldown;
    private bool IsOnCooldown
    {
        set
        {
            isOnCooldown = value;
            spriteRenderer.color = value ? Color.red : new Color(1,1,1, 0.5f);
        }
    }

    private new Collider2D collider2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        barrierClass = LoadedData.GetSavedBarrier(side, positionOnWall) ?? new BarrierClass();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        IsActive = false;
    }
    
    public void Activate()
    {
        if (isActive || isOnCooldown) return;
        
        IsActive = true;
        StartCoroutine(WaitToTurnOff());
    }

    private IEnumerator WaitToTurnOff()
    {
        yield return new WaitForSeconds(1);

        IsActive = false;
        IsOnCooldown = true;
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(Cooldown);

        IsOnCooldown = false;
    }
}
