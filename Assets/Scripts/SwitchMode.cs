using System;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    private const float FarCamera = 20;
    private const float CloseCamera = 15;

    private static Mode _mode;

    public static Mode Mode
    {
        get => _mode;
        private set
        {
            _mode = value;
            EventAggregator.ModeSwitched.Publish(value);
        }
    }

    private void Start()
    {
        TurnPlayerMode();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Tab)) return;
        
        switch (Mode)
        {
            case Mode.Ship:
                TurnPlayerMode();
                break;
            case Mode.Player:
                TurnShipMode();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void TurnPlayerMode()
    {
        Mode = Mode.Player;
        Camera.main.orthographicSize = CloseCamera;
    }

    private static void TurnShipMode()
    {
        Mode = Mode.Ship;
        Camera.main.orthographicSize = FarCamera;
    }
}

public enum Mode
{
    Ship,
    Player
}
