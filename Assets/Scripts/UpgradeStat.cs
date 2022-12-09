using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStat : MonoBehaviour
{
    [SerializeField] private TMP_Text statName;
    [SerializeField] private TMP_Text cost;
    [SerializeField] private Image rank;
    [SerializeField] private Button button;
    [SerializeField] private bool isFirstStat;

    private void Awake()
    {
        EventAggregator.ChooseUpgradeWall.Subscribe(UpgradeWall);
        EventAggregator.ChooseUpgradeTurret.Subscribe(UpgradeTurret);
        EventAggregator.ChooseUpgradeBarrier.Subscribe(UpgradeBarrier);
    }

    private void UpgradeWall(Wall wall)
    {
        if (!isFirstStat)
        {
            gameObject.SetActive(false);
            return;
        }

        button.interactable = wall.healthRank < wall.healthMaxRank;
        statName.text = "Прочность";
        cost.text = wall.healthRank < wall.healthMaxRank
            ? $"{wall.NextUpgradeCost}/{Resources.Metal.Count}"
            : "";
        rank.fillAmount = (float)wall.healthRank / wall.healthMaxRank;
    }

    private void UpgradeTurret(TurretBody turret)
    {
        gameObject.SetActive(true);
        
        if (isFirstStat)
        {
            button.interactable = turret.healthRank < turret.healthMaxRank;
            statName.text = "Прочность";
            cost.text = turret.healthRank < turret.healthMaxRank
                ? $"{turret.NextHealthUpgradeCost}/{Resources.Metal.Count}"
                : "";
            rank.fillAmount = (float)turret.healthRank / turret.healthMaxRank;
        }
        else
        {
            button.interactable = turret.damageRank < turret.damageMaxRank;
            statName.text = "Урон";
            cost.text = turret.damageRank < turret.damageMaxRank
                ? $"{turret.NextDamageUpgradeCost}/{Resources.Metal.Count}"
                : "";
            rank.fillAmount = (float)turret.healthRank / turret.healthMaxRank;
        }
    }

    private void UpgradeBarrier(Barrier barrier)
    {
        if (!isFirstStat)
        {
            gameObject.SetActive(false);
            return;
        }

        button.interactable = barrier.cooldownRank < barrier.CooldownMaxRank;
        statName.text = "Перезарядка";
        cost.text = barrier.cooldownRank < barrier.CooldownMaxRank
            ? $"{barrier.NextUpgradeCost}/{Resources.Metal.Count}"
            : "";
        rank.fillAmount = (float)barrier.cooldownRank / barrier.CooldownMaxRank;
    }

    private void OnDestroy()
    {
        EventAggregator.ChooseUpgradeWall.Unsubscribe(UpgradeWall);
        EventAggregator.ChooseUpgradeTurret.Unsubscribe(UpgradeTurret);
        EventAggregator.ChooseUpgradeBarrier.Unsubscribe(UpgradeBarrier);
    }
}
