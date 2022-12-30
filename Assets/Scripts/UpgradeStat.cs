using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
        gameObject.SetActive(false);
    }

    private void UpgradeWall(Wall wall)
    {
        if (!isFirstStat)
        {
            gameObject.SetActive(false);
            return;
        }
        
        gameObject.SetActive(true);
        button.interactable = wall.HealthRank < wall.HealthMaxRank && wall.NextUpgradeCost <= Resources.Metal.Count;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
            SetOnClick(() => wall.HealthRank += 1, () => EventAggregator.ChooseUpgradeWall.Publish(wall),
                wall.NextUpgradeCost));
        statName.text = "Прочность";
        cost.text = wall.HealthRank < wall.HealthMaxRank
            ? $"{wall.NextUpgradeCost}/{Resources.Metal.Count}"
            : "";
        cost.color = wall.NextUpgradeCost <= Resources.Metal.Count ? Color.black : Color.red;
        rank.fillAmount = (float)wall.HealthRank / wall.HealthMaxRank;
    }

    private void UpgradeTurret(TurretBody turret)
    {
        gameObject.SetActive(true);

        if (!isFirstStat)
        {
            button.interactable = turret.HealthRank < turret.HealthMaxRank && turret.NextHealthUpgradeCost <= Resources.Metal.Count;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
                SetOnClick(() => turret.HealthRank += 1, () => EventAggregator.ChooseUpgradeTurret.Publish(turret),
                    turret.NextHealthUpgradeCost));
            statName.text = "Прочность";
            cost.text = turret.HealthRank < turret.HealthMaxRank
                ? $"{turret.NextHealthUpgradeCost}/{Resources.Metal.Count}"
                : "";
            cost.color = turret.NextHealthUpgradeCost <= Resources.Metal.Count ? Color.black : Color.red;
            rank.fillAmount = (float)turret.HealthRank / turret.HealthMaxRank;
        }
        else
        {
            button.interactable = turret.DamageRank < turret.DamageMaxRank && turret.NextDamageUpgradeCost <= Resources.Metal.Count;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
                SetOnClick(() => turret.DamageRank += 1, () => EventAggregator.ChooseUpgradeTurret.Publish(turret),
                    turret.NextDamageUpgradeCost));
            statName.text = "Урон";
            cost.text = turret.DamageRank < turret.DamageMaxRank
                ? $"{turret.NextDamageUpgradeCost}/{Resources.Metal.Count}"
                : "";
            cost.color = turret.NextDamageUpgradeCost <= Resources.Metal.Count ? Color.black : Color.red;
            rank.fillAmount = (float)turret.DamageRank / turret.DamageMaxRank;
        }
    }

    private void UpgradeBarrier(Barrier barrier)
    {
        if (!isFirstStat || !barrier.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        button.interactable = barrier.CooldownRank < barrier.CooldownMaxRank && barrier.NextUpgradeCost <= Resources.Metal.Count;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
            SetOnClick(() => barrier.CooldownRank += 1, () => EventAggregator.ChooseUpgradeBarrier.Publish(barrier),
                barrier.NextUpgradeCost));
        statName.text = "Перезарядка";
        cost.text = barrier.CooldownRank < barrier.CooldownMaxRank
            ? $"{barrier.NextUpgradeCost}/{Resources.Metal.Count}"
            : "";
        cost.color = barrier.NextUpgradeCost <= Resources.Metal.Count ? Color.black : Color.red;
        rank.fillAmount = (float)barrier.CooldownRank / barrier.CooldownMaxRank;
    }

    private void SetOnClick(Action upgrade, Action eventCall, int cost)
    {
        upgrade.Invoke();
        Resources.Metal.Count -= cost;
        eventCall.Invoke();
    }

    private void OnDestroy()
    {
        EventAggregator.ChooseUpgradeWall.Unsubscribe(UpgradeWall);
        EventAggregator.ChooseUpgradeTurret.Unsubscribe(UpgradeTurret);
        EventAggregator.ChooseUpgradeBarrier.Unsubscribe(UpgradeBarrier);
    }
}
