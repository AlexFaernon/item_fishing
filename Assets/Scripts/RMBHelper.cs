using UnityEngine;

public class RMBHelper : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TurretBody turret;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventAggregator.MouseOverTurret.Subscribe(OnTurret);
    }

    private void OnTurret(TurretBody turretBody)
    {
        turret = turretBody;
        if (turretBody == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(turret.IsBarrierInstalled);
    }

    private void Update()
    {
        if (turret && turret.IsBarrierInstalled)
        {
            if (!turret.isPlayerInRange)
            {
                spriteRenderer.color = Color.gray;
                return;
            }

            spriteRenderer.color = turret.barrierScript.IsReady ? Color.white : Color.red;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        EventAggregator.MouseOverTurret.Unsubscribe(OnTurret);
    }
}
