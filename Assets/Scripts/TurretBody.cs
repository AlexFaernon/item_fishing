using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretBody : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Side side;
    [SerializeField] private Side positionOnWall;
    [SerializeField] private GameObject barrier;
    [SerializeField] private SpriteRenderer hpBar;
    [SerializeField] private SpriteRenderer hpBarLength;
    [SerializeField] private Light light;
    [HideInInspector] public Barrier barrierScript;
    public TurretClass turretClass;
    public Side Side => side;
    public Side PositionOnWall => positionOnWall;
    
    public int HealthRank
    {
        get => turretClass.HealthRank;
        set
        {
            turretClass.HealthRank = value;
            hpBarLength.size = new Vector2(turretClass.MaxHealth, 4);
        }
    }

    public int DamageRank
    {
        get => turretClass.DamageRank;
        set => turretClass.DamageRank = value;
    }

    public int MaxHealth => turretClass.MaxHealth;

    private int TimeToRepair => !IsBroken && IsInstalled ? 2 : 5;
    public int MetalToRepair => !IsBroken && IsInstalled ? 2 : 5;
    private bool isRepairing;
    public int Health
    {
        get => turretClass.Health;
        private set
        {
            turretClass.Health = value;
            hpBar.size = new Vector2(value, 4);
        }
    }

    public bool IsBroken
    {
        get => turretClass.IsBroken;
        private set
        {
            turretClass.IsBroken = value;
            turretControlScript.enabled = !value;
        }
    }

    public bool IsInstalled
    {
        get => turretClass.IsInstalled;
        private set
        {
            turretClass.IsInstalled = value;
            turretControlScript.enabled = value;
        }
    }

    public bool IsBarrierInstalled
    {
        get => turretClass.IsBarrierInstalled;
        set
        {
            turretClass.IsBarrierInstalled = value;
            barrier.SetActive(value);
        }
    }

    [HideInInspector] public bool isPlayerInRange;
    private SpriteRenderer spriteRenderer;
    private Color normalColor; //todo разгрести дерьмо
    private Turret turretControlScript;

    private void Awake()
    {
        turretClass = LoadedData.GetSavedTurret(side, positionOnWall) ?? new TurretClass();
        turretControlScript = transform.parent.gameObject.GetComponent<Turret>();
        
        EventAggregator.BarrierInstalled.Subscribe(OnBarrierInstallation);
        barrierScript = barrier.GetComponent<Barrier>();
        barrierScript.side = side;
        barrierScript.positionOnWall = positionOnWall;
        barrier.SetActive(IsBarrierInstalled);
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        hpBar.size = new Vector2(Health, 4);
        hpBarLength.size = new Vector2(turretClass.MaxHealth, 4);
        hpBar.transform.parent.gameObject.SetActive(false);
        
        if (!IsInstalled)
        {
            spriteRenderer.color = normalColor = Color.gray; //пустой слот
            turretControlScript.enabled = false;
            if (!Research.TurretsResearch)
            {
                gameObject.SetActive(false);
                EventAggregator.TurretsResearched.Subscribe(EnableOnResearch);
                return;
            }
        }

        if (IsBroken)
        {
            spriteRenderer.color = Color.black; //сломанный спрайт
            turretControlScript.enabled = false;
        }

        normalColor = spriteRenderer.color;
        Ship.AddTurret(this);
        EventAggregator.SecondLifeActivated.Subscribe(RepairOnSecondLife);
    }

    private void Update()
    {
        if (isPlayerInRange && IsBarrierInstalled && Input.GetMouseButtonDown(1))
        {
            barrierScript.Activate();
        }
        
        if (!isRepairing) return;
        
        hpBar.size += Vector2.right * Time.deltaTime / TimeToRepair;
    }

    public void WarningLight(bool isRed)
    {
        light.color = isRed ? Color.red : Color.white;
    }

    private void RepairOrInstall()
    {
        Resources.Metal.Count -= MetalToRepair;
        spriteRenderer.color = normalColor = Color.green; //нормальный спрайт
        if (IsInstalled)
        {
            Health += 1;
        }
        else
        {
            IsInstalled = true;
            EventAggregator.SecondLifeActivated.Subscribe(RepairOnSecondLife);
            Health = turretClass.MaxHealth;
        }
        IsBroken = false;
        Debug.Log("repaired");
        if (Health < turretClass.MaxHealth)
        {
            Invoke(nameof(RepairOrInstall), TimeToRepair);
            Debug.Log("repairing continued");
        }
        else
        {
            StopRepair();
        }
    }

    private void StopRepair()
    {
        hpBar.size = new Vector2(Health, 3);
        CancelInvoke(nameof(RepairOrInstall));
        isRepairing = false;
    }

    private void EnableOnResearch()
    {
        gameObject.SetActive(true);
        Ship.AddTurret(this);
    }

    private void RepairOnSecondLife()
    {
        if (!IsInstalled) return;
        
        if (Ship.Turrets.Any(turret => turret.side == Side && turret.IsInstalled && !turret.IsBroken)) return;

        IsBroken = false;
        Health = turretClass.MaxHealth;
    }

    private void OnBarrierInstallation(Barrier other)
    {
        if (barrierScript == other)
        {
            IsBarrierInstalled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            return;
        }

        if (!col.gameObject.CompareTag("Enemy") || IsBroken || !IsInstalled) return;
        
        Health -= 1;
        Debug.Log($"turret hp is {Health}");
        if (Health > 0) return;
        
        IsBroken = true;
        spriteRenderer.color = normalColor = Color.black;
        turretControlScript.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        isPlayerInRange = false;
        StopRepair();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameMode.Mode == Mode.Ship) return;
        
        spriteRenderer.color = Color.yellow;
        hpBar.transform.parent.gameObject.SetActive(IsInstalled);
        EventAggregator.MouseOverTurret.Publish(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.color = normalColor;
        hpBar.transform.parent.gameObject.SetActive(false);
        StopRepair();
        EventAggregator.MouseOverTurret.Publish(null);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isPlayerInRange || Health == turretClass.MaxHealth) return;

        if (!IsInstalled && !Research.TwoTurretsResearch &&
            Ship.Turrets.Any(turret => turret.side == Side && turret.IsInstalled))
        {
            Debug.Log("Cant install");
            return;
        }
        
        Invoke(nameof(RepairOrInstall), TimeToRepair);
        isRepairing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopRepair();
    }

    private void OnDestroy()
    {
        EventAggregator.BarrierInstalled.Unsubscribe(OnBarrierInstallation);
        EventAggregator.TurretsResearched.Unsubscribe(EnableOnResearch);
        EventAggregator.SecondLifeActivated.Unsubscribe(RepairOnSecondLife);

    }
}