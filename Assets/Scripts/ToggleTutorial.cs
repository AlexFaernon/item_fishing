using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTutorial : MonoBehaviour
{
    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggle);
        toggle.isOn = TutorialManager.TutorialEnabled;
    }

    private void OnToggle(bool isOn)
    {
        TutorialManager.TutorialEnabled = isOn;
        PlayerPrefs.SetInt(nameof(TutorialManager.TutorialEnabled), Convert.ToInt32(TutorialManager.TutorialEnabled));
    }
}
