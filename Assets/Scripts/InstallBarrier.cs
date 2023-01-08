using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstallBarrier : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;
    private Barrier barrier;

    private void Awake()
    {
        EventAggregator.ChooseUpgradeWall.Subscribe(OnNonBarrier);
        EventAggregator.ChooseUpgradeTurret.Subscribe(OnNonBarrier);
        EventAggregator.ChooseUpgradeBarrier.Subscribe(ShowBarrierInstallation);
        button.onClick.AddListener(OnClick);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        text.text = $"{Resources.Metal.Count}/10";
        button.interactable = Resources.Metal.Count >= 10;
    }

    private void OnClick()
    {
        EventAggregator.BarrierInstalled.Publish(barrier);
        EventAggregator.ChooseUpgradeBarrier.Publish(barrier);
        Resources.Metal.Count -= 10;
    }

    private void ShowBarrierInstallation(Barrier otherBarrier)
    {
        if (otherBarrier.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
        
        gameObject.SetActive(true);
        barrier = otherBarrier;
    }

    private void OnNonBarrier(object o)
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventAggregator.ChooseUpgradeWall.Unsubscribe(OnNonBarrier);
        EventAggregator.ChooseUpgradeTurret.Unsubscribe(OnNonBarrier);
        EventAggregator.ChooseUpgradeBarrier.Unsubscribe(ShowBarrierInstallation);
    }
}
