using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerUps
{
    public static int Tokens { get; private set; }
    public static bool SecondLife { get; private set; }
    public static bool TurretsBoost { get; private set; }

    public static void AddToken()
    {
        Tokens++;
    }

    public static void ActivateSecondLife()
    {
        SecondLife = true;
        Tokens -= 2;
    }

    public static void ActivateTurretBoost()
    {
        TurretsBoost = true;
        Tokens -= 1;
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
