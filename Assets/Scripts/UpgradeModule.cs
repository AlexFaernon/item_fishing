using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        upgradingModule.TryGetComponent(out wall);
        upgradingModule.TryGetComponent(out turret);
        upgradingModule.TryGetComponent(out barrier);
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (wall)
        {
            
        }

        if (turret)
        {
            
        }

        if (barrier)
        {
            
        }
    }
}