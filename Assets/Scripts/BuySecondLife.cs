using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuySecondLife : MonoBehaviour
{
    private Button button;
    private TMP_Text text;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        text = transform.GetChild(0).GetComponent<TMP_Text>();
        button.onClick.AddListener(Buy);
    }

    private void Update()
    {
        if (PowerUps.SecondLife)
        {
            text.text = "Куплено";
        }

        button.interactable = !PowerUps.SecondLife && Resources.Coins >= 2;
    }

    private void Buy()
    {
        PowerUps.ActivateSecondLife();
    }
}
