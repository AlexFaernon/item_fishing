using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretBody : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Side side;
    [SerializeField] private GameObject barrier;
    [SerializeField] private SpriteRenderer hpBar;
    [SerializeField] private SpriteRenderer hpBarLength;
    public bool isBarrierInstalled; 
    [NonSerialized] public Barrier barrierScript;
    public Side Side => side;
    
    private int healthRank;
    public int HealthRank
    {
        get => healthRank;
        set
        {
            healthRank = value;
            hpBarLength.size = new Vector2(MaxHealth, 4);
        }
    }
    public int HealthMaxRank => new[] {2, 3}.Length - 1;
    public int MaxHealth => new[]{2, 3}[HealthRank];
    public int NextHealthUpgradeCost => new[] { 10, 0 }[HealthRank];
    private int health = 5;

    [NonSerialized] public int DamageRank;
    public int DamageMaxRank => new[] { 10, 15, 20, 25 }.Length - 1;
    public int NextDamageUpgradeCost => new[] { 10, 20, 30, 0 }[HealthRank];
    public int Damage => new[] { 10, 15, 20, 25 }[DamageRank];
    
    private int TimeToRepair => !IsBroken && IsInstalled ? 2 : 5;
    public int MetalToRepair => !IsBroken && IsInstalled ? 2 : 5;
    private bool isRepairing;
    public int Health
    {
        get => health;
        private set
        {
            health = value;
            hpBar.size = new Vector2(value, 4);
        }
    }

    public bool IsBroken
    {
        get => isBroken;
        private set
        {
            isBroken = value;
            transform.parent.GetComponent<Turret>().enabled = value;
        }
    }

    public bool IsInstalled
    {
        get => isInstalled;
        private set
        {
            isInstalled = value;
            transform.parent.GetComponent<Turret>().enabled = value;
        }
    }

    private bool isInstalled;
    private bool isBroken;
    [NonSerialized] public bool IsPlayerInRange;
    private SpriteRenderer spriteRenderer;
    private Color normalColor; //todo разгрести дерьмо

    private void Awake()
    {
        barrierScript = barrier.GetComponent<Barrier>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpBar.transform.parent.gameObject.SetActive(false);
        Health = MaxHealth;
        if (!IsInstalled)
        {
            Health = 0;
            barrier.SetActive(false);
            spriteRenderer.color = normalColor = Color.gray; //пустой слот
            transform.parent.GetComponent<Turret>().enabled = false;
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
            transform.parent.GetComponent<Turret>().enabled = false;
        }

        normalColor = spriteRenderer.color;
        Ship.AddTurret(this);
        barrier.SetActive(isBarrierInstalled);
    }

    private void Update()
    {
        if (IsPlayerInRange && Input.GetMouseButtonDown(1))
        {
            barrierScript.Activate();
        }
        
        if (!isRepairing) return;
        
        hpBar.size += Vector2.right * Time.deltaTime / TimeToRepair;
    }

    private void Repair()
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
            Health = MaxHealth;
        }
        IsBroken = false;
        Debug.Log("repaired");
        if (Health < MaxHealth)
        {
            Invoke(nameof(Repair), TimeToRepair);
            Debug.Log("repairing continued");
        }
    }

    private void StopRepair()
    {
        hpBar.size = new Vector2(Health, 3);
        CancelInvoke(nameof(Repair));
        isRepairing = false;
    }

    private void EnableOnResearch()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            IsPlayerInRange = true;
            return;
        }
        
        if (!col.gameObject.CompareTag("Enemy")) return;
        
        Health -= 1;
        Debug.Log($"turret hp is {Health}");
        if (Health > 0) return;
        
        IsBroken = true;
        spriteRenderer.color = normalColor = Color.black;
        transform.parent.GetComponent<Turret>().enabled = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        IsPlayerInRange = false;
        StopRepair();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        spriteRenderer.color = Color.yellow;
        hpBar.transform.parent.gameObject.SetActive(isInstalled);
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
        if (!IsPlayerInRange || Health == MaxHealth) return;
        
        Invoke(nameof(Repair), TimeToRepair);
        isRepairing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopRepair();
    }

    private void OnDestroy()
    {
        EventAggregator.TurretsResearched.Unsubscribe(EnableOnResearch);
    }
}