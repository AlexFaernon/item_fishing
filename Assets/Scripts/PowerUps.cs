using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerUps
{
    public static bool SecondLife { get; private set; }
    public static bool TurretsBoost { get; private set; }

    public static void AddToken()
    {
        Resources.Coins += 2;
    }

    public static void BuySecondLife()
    {
        SecondLife = true;
        Resources.Coins -= 2;
    }

    public static void BuyTurretBoost()
    {
        TurretsBoost = true;
        Resources.Coins -= 1;
    }

    public static void DeactivateTurretBoost()
    {
        TurretsBoost = false;
    }

    public static void UseSecondLife()
    {
        SecondLife = false;
    }
}
