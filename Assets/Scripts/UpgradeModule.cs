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
}