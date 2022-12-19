using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;
    private float timeScale;

    private void OnEnable()
    {
        timeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = timeScale;
    }
}
