using TMPro;
using UnityEngine;

public class LMBHelper : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private SpriteRenderer spriteRenderer;
    private TurretBody turret;
    private Wall wall;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventAggregator.MouseOverTurret.Subscribe(OnTurret);
        EventAggregator.MouseOverWall.Subscribe(OnWall);
        SetActive(false);
    }

    private void OnTurret(TurretBody turretBody)
    {
        turret = turretBody;
    }

    private void OnWall(Wall wall)
    {
        this.wall = wall;
    }

    private void Update()
    {
        if (turret)
        {
            SetActive(true);
            
            if (turret.Health < turret.TurretClass.MaxHealth)
            {
                SetValues(turret.MetalToRepair, turret.IsPlayerInRange);
            }
            else
            {
                SetActive(false);
            }
            return;
        }

        if (wall)
        {
            SetActive(true);
            
            if (wall.Health < wall.wallClass.MaxHealth)
            {
                SetValues(wall.metalToRepair, wall.isPlayerInRange);
            }
            else
            {
                SetActive(false);
            }
            return;
        }
        
        SetActive(false);
    }

    private void SetValues(int metalToRepair, bool isPlayerInRange)
    {
        text.text = metalToRepair.ToString();
        text.color = metalToRepair <= Resources.Metal.Count ? Color.black : Color.red;
        spriteRenderer.color = isPlayerInRange ? Color.white : Color.gray;
    }

    private void SetActive(bool isActive)
    {
        spriteRenderer.color = isActive ? Color.white : Color.clear;
        foreach (Transform obj in transform)
        {
            obj.gameObject.SetActive(isActive);
        }
    }

    private void OnDestroy()
    {
        EventAggregator.MouseOverTurret.Unsubscribe(OnTurret);
        EventAggregator.MouseOverWall.Unsubscribe(OnWall);
    }
}
