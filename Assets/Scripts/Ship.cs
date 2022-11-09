using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public static class Ship
{
    private static readonly List<TurretBody> Turrets = new();
    private static readonly Random Random = new (); 

    public static void AddTurret(TurretBody turret)
    {
        Turrets.Add(turret);
    }

    public static GameObject GetTarget(Side side)
    {
        var turretsOnSide = Turrets.Where(turret => turret.Side == side && !turret.isBroken).ToList();
        if (turretsOnSide.Count > 0)
        {
            return turretsOnSide[Random.Next(turretsOnSide.Count)].gameObject;
        }

        throw new NotImplementedException();
    }
}

public enum Side
{
    Up,
    Right,
    Down,
    Left
}