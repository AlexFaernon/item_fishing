using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject exitConfirm;
    private float timeScale;

    private void Awake()
    {
        continueButton.onClick.AddListener(() => gameObject.SetActive(false));
        exitButton.onClick.AddListener(() => exitConfirm.SetActive(true));
    }

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
