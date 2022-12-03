using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Barrier : MonoBehaviour
{
    private bool isActive;
    private bool IsActive
    {
        set
        {
            isActive = value;
            spriteRenderer.color = value ? new Color(0, 0, 1f, 0.7f) : new Color(0, 0, 1f, 0.2f);
            collider2D.enabled = value;
        }
    }

    private bool isOnCooldown;
    private bool IsOnCooldown
    {
        set
        {
            isOnCooldown = value;
            spriteRenderer.color = value ? new Color(1f, 0, 0, 0.1f) : new Color(0, 0, 1f, 0.2f);
        }
    }

    private new Collider2D collider2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
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
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);

        IsOnCooldown = false;
    }
}
