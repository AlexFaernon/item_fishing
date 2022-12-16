using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTypeButton : MonoBehaviour
{
    [SerializeField] private UpgradeType upgradeType;
    private Button button;
    private Image image;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(OnClick);
        EventAggregator.ChooseUpgradeType.Subscribe(TurnInteractable);

        switch (upgradeType)
        {
            case UpgradeType.Wall:
                break;
            case UpgradeType.Turret:
                if (!Research.TurretsResearch)
                {
                    button.interactable = false;
                    EventAggregator.TurretsResearched.Subscribe(ActivateOnResearch);
                }
                break;
            case UpgradeType.Barrier:
                if (!Research.BarriersResearch)
                {
                    button.interactable = false;
                    EventAggregator.BarriersResearched.Subscribe(ActivateOnResearch);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnClick()
    {
        EventAggregator.ChooseUpgradeType.Publish(upgradeType);
    }

    private void ActivateOnResearch()
    {
        button.interactable = true;
    }
    
    private void TurnInteractable(UpgradeType otherUpgradeType)
    {
        image.color = upgradeType == otherUpgradeType ? Color.black : Color.gray;
    }

    private void OnDestroy()
    {
        EventAggregator.ChooseUpgradeType.Unsubscribe(TurnInteractable);
        EventAggregator.TurretsResearched.Unsubscribe(ActivateOnResearch);
        EventAggregator.BarriersResearched.Unsubscribe(ActivateOnResearch);
    }
}

public enum UpgradeType
{
    Wall,
    Turret,
    Barrier
}