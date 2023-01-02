using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoTurrets : MonoBehaviour
{
    [SerializeField] private Side side;
    [SerializeField] private Side positionOnWall;
    private Image spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<Image>();
    }

    private void OnEnable()
    {
        var turret = LoadedData.GetSavedTurret(side, positionOnWall);
        if (turret is null)
        {
            spriteRenderer.color = Color.clear;
            return;
        }
        spriteRenderer.color = ((double)turret.Health / turret.MaxHealth * 100) switch
        {
            0 => Color.black,
            >= 66 => Color.green,
            < 66 and >= 33 => Color.yellow,
            < 33 => Color.red,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
