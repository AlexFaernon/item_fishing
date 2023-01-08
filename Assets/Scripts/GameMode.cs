using System;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static bool ShouldSwitchToShip;
    
    private const float FarCamera = 20;
    private const float CloseCamera = 15;

    private static Mode _mode;

    public static Mode Mode
    {
        get => _mode;
        set
        {
            _mode = value;
            switch (value)
            {
                case Mode.Player:
                    TurnPlayerMode();
                    break;
                case Mode.Fishing:
                    TurnShipMode();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            EventAggregator.ModeSwitched.Publish(value);
        }
    }

    private void Start()
    {
        Mode = Mode.Player;
    }

    private void Update()
    {
        if (!(Input.GetKeyDown(KeyCode.Q) || ShouldSwitchToShip) || Mode == Mode.Player || Hook.IsLaunched ||
            Hook.IsRetracting) return;

        ShouldSwitchToShip = false;
        Mode = Mode.Player;
    }

    private static void TurnPlayerMode()
    {
        Camera.main.orthographicSize = CloseCamera;
    }

    private static void TurnShipMode()
    {
        Camera.main.orthographicSize = FarCamera;
    }
}

public enum Mode
{
    Fishing,
    Player
}
