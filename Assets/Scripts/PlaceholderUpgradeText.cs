using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderUpgradeText : MonoBehaviour
{
    private void Awake()
    {
        EventAggregator.ChooseUpgradeType.Subscribe(Activate);
        EventAggregator.ChooseUpgradeWall.Subscribe(Deactivate);
        EventAggregator.ChooseUpgradeTurret.Subscribe(Deactivate);
        EventAggregator.ChooseUpgradeBarrier.Subscribe(Deactivate);
    }

    private void Activate(UpgradeType upgradeType)
    {
        gameObject.SetActive(true);
    }

    private void Deactivate(object o)
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventAggregator.ChooseUpgradeType.Unsubscribe(Activate);
        EventAggregator.ChooseUpgradeWall.Unsubscribe(Deactivate);
        EventAggregator.ChooseUpgradeTurret.Unsubscribe(Deactivate);
        EventAggregator.ChooseUpgradeBarrier.Unsubscribe(Deactivate);
    }
}
