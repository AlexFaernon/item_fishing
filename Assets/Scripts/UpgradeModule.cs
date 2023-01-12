using UnityEngine;
using UnityEngine.UI;

public class UpgradeModule : MonoBehaviour
{
    [SerializeField] private GameObject upgradingModule;
    private Button button;
    private Wall wall;
    private TurretBody turret;
    private Barrier barrier;
    private UpgradeType? upgradeType;
    private Image image;

    private void Awake()
    {
        if (upgradingModule == null)
        {
            return;
        }

        image = GetComponent<Image>();
        upgradingModule.TryGetComponent(out wall);
        upgradingModule.TryGetComponent(out turret);
        upgradingModule.TryGetComponent(out barrier);
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        EventAggregator.ChooseUpgradeType.Subscribe(SetUpgradeType);
        EventAggregator.ChooseUpgradeBarrier.Subscribe(Deactivate);
        EventAggregator.ChooseUpgradeTurret.Subscribe(Deactivate);
        EventAggregator.ChooseUpgradeWall.Subscribe(Deactivate);
        button.interactable = false;
    }

    private void SetUpgradeType(UpgradeType other)
    {
        image.color = Color.white;
        upgradeType = other;
    }

    private void Deactivate(object o)
    {
        image.color = Color.white;
    }

    private void Update()
    {
        if (wall && upgradeType == UpgradeType.Wall ||
            turret && turret.IsInstalled && upgradeType == UpgradeType.Turret ||
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
            image.color = Color.yellow;
        }

        if (turret)
        {
            EventAggregator.ChooseUpgradeTurret.Publish(turret);
            image.color = Color.yellow;
        }

        if (barrier)
        {
            EventAggregator.ChooseUpgradeBarrier.Publish(barrier);
            image.color = Color.yellow;
        }
    }

    private void OnDestroy()
    {
        EventAggregator.ChooseUpgradeType.Unsubscribe(SetUpgradeType);
        EventAggregator.ChooseUpgradeBarrier.Unsubscribe(Deactivate);
        EventAggregator.ChooseUpgradeTurret.Unsubscribe(Deactivate);
        EventAggregator.ChooseUpgradeWall.Unsubscribe(Deactivate);
    }
}