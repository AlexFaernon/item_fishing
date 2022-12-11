using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeModule : MonoBehaviour
{
    [SerializeField] private GameObject upgradingModule;
    private Button button;
    private Wall wall;
    private TurretBody turret;
    private Barrier barrier;

    private void Awake()
    {
        if (upgradingModule == null)
        {
            return;
        }
        
        upgradingModule.TryGetComponent(out wall);
        upgradingModule.TryGetComponent(out turret);
        upgradingModule.TryGetComponent(out barrier);
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        EventAggregator.ChooseUpgradeType.Subscribe(SetInteractable);
        button.interactable = false;
    }

    private void SetInteractable(UpgradeType upgradeType)
    {
        if (wall && upgradeType == UpgradeType.Wall || turret && upgradeType == UpgradeType.Turret ||
            barrier && upgradeType == UpgradeType.Barrier)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    private void OnClick()
    {
        if (wall)
        {
            EventAggregator.ChooseUpgradeWall.Publish(wall);
        }

        if (turret)
        {
            EventAggregator.ChooseUpgradeTurret.Publish(turret);
        }

        if (barrier)
        {
            EventAggregator.ChooseUpgradeBarrier.Publish(barrier);
        }
    }

    private void OnDestroy()
    {
        EventAggregator.ChooseUpgradeType.Unsubscribe(SetInteractable);
    }
}