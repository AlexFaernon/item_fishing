using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretBody : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Side side;
    [SerializeField] private GameObject barrier;
    [SerializeField] private SpriteRenderer hpBar;
    public bool isBarrierInstalled;
    private Barrier barrierScript;
    public Side Side => side;
    private int maxHealth = 5;
    private int health = 5;
    private const int TimeToRepair = 2;
    private const int MetalToRepair = 2;
    private bool isRepairing;
    public int Health
    {
        get => health;
        private set
        {
            health = value;
            hpBar.size = new Vector2(value, 3);
        }
    }
    
    public bool isBroken;
    private bool isPlayerInRange;
    private SpriteRenderer spriteRenderer;
    private Color normalColor;

    private void Awake()
    {
        Ship.AddTurret(this);
        barrier.SetActive(isBarrierInstalled);
        barrierScript = barrier.GetComponent<Barrier>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;
        hpBar.transform.parent.gameObject.SetActive(false);
        Health = 1;
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetMouseButtonDown(1))
        {
            barrierScript.Activate();
        }
        
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
        Debug.Log($"turret hp is {Health}");
        if (Health > 0) return;
        
        isBroken = true;
        transform.parent.GetComponent<Turret>().enabled = false;
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.color = normalColor;
        hpBar.transform.parent.gameObject.SetActive(false);
        StopRepair();
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