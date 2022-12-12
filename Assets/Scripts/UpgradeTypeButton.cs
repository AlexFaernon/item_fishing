using UnityEngine;
using UnityEngine.UI;

public class UpgradeTypeButton : MonoBehaviour
{
    [SerializeField] private UpgradeType upgradeType;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        EventAggregator.ChooseUpgradeType.Subscribe(TurnInteractable);
    }

    private void OnClick()
    {
        EventAggregator.ChooseUpgradeType.Publish(upgradeType);
    }

    private void TurnInteractable(UpgradeType otherUpgradeType)
    {
        button.interactable = otherUpgradeType != upgradeType;
    }

    private void OnDestroy()
    {
        EventAggregator.ChooseUpgradeType.Unsubscribe(TurnInteractable);
    }
}

public enum UpgradeType
{
    Wall,
    Turret,
    Barrier
}