using System;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    private const float FarCamera = 15;
    private const float CloseCamera = 5;

    private static Mode _mode;

    public static Mode Mode
    {
        get => _mode;
        set
        {
            _mode = value;
            EventAggregator.ModeSwitched.Publish(value);
        }
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Tab)) return;
        
        switch (Mode)
        {
            case Mode.Ship:
                Mode = Mode.Player;
                Camera.main.orthographicSize = CloseCamera;
                break;
            case Mode.Player:
                Mode = Mode.Ship;
                Camera.main.orthographicSize = FarCamera;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum Mode
{
    Ship,
    Player
}
