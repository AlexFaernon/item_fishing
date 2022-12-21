using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wall : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Side side;
    [SerializeField] private SpriteRenderer hpBar;
    [SerializeField] private SpriteRenderer hpBarLength;
    private SpriteRenderer spriteRenderer;
    private Color normalColor;
    public Side Side => side;

    [NonSerialized] public int metalToRepair = 2;
    private const int TimeToRepair = 3;
    private bool isRepairing;

    private int healthRank;
    public int HealthRank
    {
        get => healthRank;
        set
        {
            healthRank = value;
            hpBarLength.size = new Vector2(MaxHealth, 1);
        }
    }
    public int HealthMaxRank => new[] { 2, 3, 4, 5 }.Length - 1;
    public int MaxHealth => new[]{2, 3, 4, 5}[HealthRank];
    public int NextUpgradeCost => new[] { 10, 20, 30, 0 }[HealthRank];
    private int health = 5;
    public bool isPlayerInRange;

    public int Health
    {
        get => health;
        private set
        {
            health = value;
            hpBar.size = new Vector2(value, 1);
        }
    }

    private void Awake()
    {
        Ship.AddWall(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;
        hpBar.transform.parent.gameObject.SetActive(false);
        HealthRank = 0;
        Health = MaxHealth;
        EventAggregator.SecondLifeActivated.Subscribe(RepairOnSecondLife);
    }

    private void Update()
    {
        if (!isRepairing) return;
        
        hpBar.size += Vector2.right * Time.deltaTime / TimeToRepair;
    }

    private void Repair()
    {
        Health += 1;
        Resources.Metal.Count -= metalToRepair;
        Debug.Log("repaired");
        if (Health < MaxHealth)
        {
            Invoke(nameof(Repair), TimeToRepair);
            Debug.Log("repairing continued");
        }
    }

    private void RepairOnSecondLife()
    {
        Health = MaxHealth;
    }

    private void StopRepair()
    {
        hpBar.size = new Vector2(Health, 3);
        CancelInvoke(nameof(Repair));
        isRepairing = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            return;
        }
        
        if (!col.gameObject.CompareTag("Enemy")) return;

        Health -= 1;
        Debug.Log($"wall hp is {Health}");
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
        hpBar.transform.parent.gameObject.SetActive(true);
        EventAggregator.MouseOverWall.Publish(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.color = normalColor;
        hpBar.transform.parent.gameObject.SetActive(false);
        StopRepair();
        EventAggregator.MouseOverWall.Publish(null);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isPlayerInRange || Health == MaxHealth) return;
        
        Invoke(nameof(Repair), TimeToRepair);
        isRepairing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopRepair();
    }

    private void OnDestroy()
    {
        EventAggregator.SecondLifeActivated.Unsubscribe(RepairOnSecondLife);

    }
}