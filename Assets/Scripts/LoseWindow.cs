using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour
{
    [SerializeField] private Button replayButton;
    [SerializeField] private Button mainMenuButton;
    private void Awake()
    {
        Time.timeScale = 0;
        replayButton.onClick.AddListener(Replay);
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }

    private void Replay()
    {
        Level.ReloadLevel();
        SceneManager.LoadScene("SampleScene");
    }
}
