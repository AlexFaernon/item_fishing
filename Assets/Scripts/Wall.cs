using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wall : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Side side;
    [SerializeField] private SpriteRenderer hpBar;
    private SpriteRenderer spriteRenderer;
    private Color normalColor;
    public Side Side => side;

    public int MetalToRepair = 2;
    private const int TimeToRepair = 3;
    private bool isRepairing;
    public int maxHealth = 5;
    private int health = 5;
    private bool isHovered;
    private float holdTime;
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
        Health = 2;

    }

    private void Update()
    {
        if (!isRepairing) return;
        
        hpBar.size += Vector2.right * Time.deltaTime / TimeToRepair;
    }

    private void Repair()
    {
        Health += 1;
        Resources.Metal.Count -= MetalToRepair;
        Debug.Log("repaired");
        if (Health < maxHealth)
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
        if (!isPlayerInRange || Health == maxHealth) return;
        
        Invoke(nameof(Repair), TimeToRepair);
        isRepairing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopRepair();
    }

}
