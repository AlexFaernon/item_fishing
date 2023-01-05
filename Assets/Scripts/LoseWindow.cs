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
        replayButton.onClick.AddListener(() => SceneManager.LoadScene("SampleScene"));
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }
}
