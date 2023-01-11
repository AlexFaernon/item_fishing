using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretBody : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Side side;
    [SerializeField] private Side positionOnWall;
    [SerializeField] private GameObject barrier;
    [SerializeField] private SpriteRenderer hpBar;
    [SerializeField] private SpriteRenderer hpBarLength;
    [SerializeField] private new Light light;
    [SerializeField] private Sprite notInstalledSprite;
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private GameObject installBar;
    [SerializeField] private GameObject cantInstallWin;
    private Sprite normalSprite;
    private SpriteRenderer installFill;
    
    [HideInInspector] public Barrier barrierScript;
    public TurretClass TurretClass;
    public Side Side => side;
    public Side PositionOnWall => positionOnWall;
    
    public int HealthRank
    {
        get => TurretClass.HealthRank;
        set
        {
            TurretClass.HealthRank = value;
            hpBarLength.size = new Vector2(TurretClass.MaxHealth, 4);
        }
    }

    public int DamageRank
    {
        get => TurretClass.DamageRank;
        set => TurretClass.DamageRank = value;
    }

    public int MaxHealth => TurretClass.MaxHealth;

    private int TimeToRepair => !IsBroken && IsInstalled ? 2 : 5;
    public int MetalToRepair => !IsBroken && IsInstalled ? 2 : 5;
    private bool isRepairing;
    private bool isInstalling;
    public int Health
    {
        get => TurretClass.Health;
        private set
        {
            TurretClass.Health = value;
            hpBar.size = new Vector2(value, 4);
            hpBar.color = HPBarColor;
        }
    }

    private Color HPBarColor
    {
        get
        {
            return ((double)Health / MaxHealth) switch
            {
                >= 0.6 => Color.green,
                < 0.6 and >= 0.3 => Color.yellow,
                < 0.3 => Color.red,
                _ => throw new ArgumentException()
            };
        }
    }

    public bool IsBroken
    {
        get => TurretClass.IsBroken;
        private set
        {
            TurretClass.IsBroken = value;
            turretControlScript.enabled = !value;
            light.enabled = !value;
            spriteRenderer.sprite = value ? brokenSprite : normalSprite;
        }
    }

    public bool IsInstalled
    {
        get => TurretClass.IsInstalled;
        private set
        {
            TurretClass.IsInstalled = value;
            turretControlScript.enabled = value;
            light.enabled = value;
            installBar.SetActive(false);
        }
    }

    public bool IsBarrierInstalled
    {
        get => TurretClass.IsBarrierInstalled;
        private set
        {
            TurretClass.IsBarrierInstalled = value;
            barrier.SetActive(value);
        }
    }

    [HideInInspector] public bool isPlayerInRange;
    private SpriteRenderer spriteRenderer;
    private Turret turretControlScript;

    private void Awake()
    {
        TurretClass = LoadedData.GetSavedTurret(side, positionOnWall) ?? new TurretClass();
        turretControlScript = transform.parent.gameObject.GetComponent<Turret>();
        
        EventAggregator.BarrierInstalled.Subscribe(OnBarrierInstallation);
        barrierScript = barrier.GetComponent<Barrier>();
        barrierScript.side = side;
        barrierScript.positionOnWall = positionOnWall;
        barrier.SetActive(IsBarrierInstalled);
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalSprite = spriteRenderer.sprite;
        
        hpBar.size = new Vector2(Health, 4);
        hpBarLength.size = new Vector2(TurretClass.MaxHealth, 4);
        hpBar.color = HPBarColor;
        hpBar.transform.parent.gameObject.SetActive(false);

        installFill = installBar.transform.GetChild(0).GetComponent<SpriteRenderer>();
        installBar.SetActive(false);
        
        if (!IsInstalled)
        {
            spriteRenderer.sprite = notInstalledSprite;
            turretControlScript.enabled = false;
            light.enabled = false;
            if (!Research.TurretsResearch)
            {
                gameObject.SetActive(false);
                EventAggregator.TurretsResearched.Subscribe(EnableOnResearch);
                return;
            }
        }

        if (IsBroken)
        {
            spriteRenderer.sprite = brokenSprite;
            turretControlScript.enabled = false;
            light.enabled = false;
        }
        
        Ship.AddTurret(this);
        EventAggregator.SecondLifeActivated.Subscribe(RepairOnSecondLife);
    }

    private void Update()
    {
        if (isRepairing)
        {
            hpBar.size += Vector2.right * Time.deltaTime / TimeToRepair;
        }

        if (isInstalling)
        {
            installFill.size += Vector2.right * Time.deltaTime / TimeToRepair;
        }
    }

    public void WarningLight(bool isRed)
    {
        light.color = isRed ? Color.red : Color.white;
    }

    private void RepairOrInstall()
    {
        Resources.Metal.Count -= MetalToRepair;
        if (IsInstalled)
        {
            Health += 1;
        }
        else
        {
            IsInstalled = true;
            EventAggregator.SecondLifeActivated.Subscribe(RepairOnSecondLife);
            Health = TurretClass.MaxHealth;
        }
        IsBroken = false;
        if (Health < TurretClass.MaxHealth)
        {
            Invoke(nameof(RepairOrInstall), TimeToRepair);
        }
        else
        {
            StopRepair();
        }
    }

    private void StopRepair()
    {
        hpBar.size = new Vector2(Health, 3);
        installFill.size = Vector2.up;
        CancelInvoke(nameof(RepairOrInstall));
        isRepairing = false;
        isInstalling = false;
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
        Health = TurretClass.MaxHealth;
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
        if (GameMode.Mode == Mode.Fishing) return;
        
        spriteRenderer.color = Color.yellow;
        hpBar.transform.parent.gameObject.SetActive(IsInstalled);
        installBar.SetActive(!IsInstalled);
        EventAggregator.MouseOverTurret.Publish(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.color = Color.white;
        hpBar.transform.parent.gameObject.SetActive(false);
        installBar.SetActive(false);
        StopRepair();
        EventAggregator.MouseOverTurret.Publish(null);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && isPlayerInRange && IsBarrierInstalled)
        {
            barrierScript.Activate();
        }

        if (!isPlayerInRange || Health == TurretClass.MaxHealth || Resources.Metal.Count < MetalToRepair ||
            eventData.button != PointerEventData.InputButton.Left) return;

        if (!IsInstalled && !Research.TwoTurretsResearch &&
            Ship.Turrets.Any(turret => turret.side == Side && turret.IsInstalled))
        {
            StopAllCoroutines();
            StartCoroutine(CantInstallWindowShow());
            return;
        }
        
        if (IsInstalled)
        {
            isRepairing = true;
        }
        else
        {
            isInstalling = true;
        }
        Invoke(nameof(RepairOrInstall), TimeToRepair);
    }

    private IEnumerator CantInstallWindowShow()
    {
        cantInstallWin.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        cantInstallWin.SetActive(false);
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