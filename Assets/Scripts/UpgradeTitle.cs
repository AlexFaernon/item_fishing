using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTitle : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image icon;

    [SerializeField] private Sprite wallIcon;
    [SerializeField] private Sprite turretIcon;
    [SerializeField] private Sprite barrierIcon;

    private void Awake()
    {
        EventAggregator.ChooseUpgradeType.Subscribe(SetUpgradeName);
        text.text = "";
        icon.sprite = wallIcon;
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
        text.text= upgradeType switch
        {
            UpgradeType.Wall => "Стена",
            UpgradeType.Turret => "Турель",
            UpgradeType.Barrier => "Барьер",
            _ => throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null)
        };
        icon.sprite = upgradeType switch
        {
            UpgradeType.Wall => wallIcon,
            UpgradeType.Turret => turretIcon,
            UpgradeType.Barrier => barrierIcon,
            _ => throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null)
        };
    }

    private void OnDestroy()
    {
        EventAggregator.ChooseUpgradeType.Unsubscribe(SetUpgradeName);
    }
}
