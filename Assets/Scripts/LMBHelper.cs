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
        gameObject.SetActive(false);
    }

    private void OnTurret(TurretBody turretBody)
    {
        turret = turretBody;
        if (turretBody == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
    }

    private void OnWall(Wall wall)
    {
        this.wall = wall;
        if (wall == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (turret)
        {
            if (turret.Health < turret.MaxHealth)
            {
                SetValues(turret.MetalToRepair, turret.IsPlayerInRange);
            }
            else
            {
                gameObject.SetActive(false);
            }
            return;
        }

        if (wall)
        {
            if (wall.Health < wall.MaxHealth)
            {
                SetValues(wall.metalToRepair, wall.isPlayerInRange);
            }
            else
            {
                gameObject.SetActive(false);
            }
            return;
        }
        
        gameObject.SetActive(false);
    }

    private void SetValues(int metalToRepair, bool isPlayerInRange)
    {
        text.text = metalToRepair.ToString();
        text.color = metalToRepair <= Resources.Metal.Count ? Color.black : Color.red;
        spriteRenderer.color = isPlayerInRange ? Color.white : Color.gray;
    }

    private void OnDestroy()
    {
        EventAggregator.MouseOverTurret.Unsubscribe(OnTurret);
        EventAggregator.MouseOverWall.Unsubscribe(OnWall);
    }
}
