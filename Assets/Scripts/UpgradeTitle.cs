using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeTitle : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private void Awake()
    {
        EventAggregator.ChooseUpgradeType.Subscribe(SetUpgradeName);
        text.text = "";
    }
    
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = TimerOnClick.TimeScale;
    }

    private void SetUpgradeName(UpgradeType upgradeType)
    {
        text.text = upgradeType switch
        {
            UpgradeType.Wall => "Стена",
            UpgradeType.Turret => "Турель",
            UpgradeType.Barrier => "Барьер",
            _ => throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null)
        };
    }

    private void OnDestroy()
    {
        EventAggregator.ChooseUpgradeType.Unsubscribe(SetUpgradeName);
    }
}
