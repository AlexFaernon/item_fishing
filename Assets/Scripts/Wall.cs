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
    [SerializeField] private GameObject gameOver;
    public WallClass wallClass;
    private SpriteRenderer spriteRenderer;
    private Color normalColor;
    public Side Side => side;
    
    [HideInInspector] public int metalToRepair = 2;
    private const int TimeToRepair = 3;
    private bool isRepairing;
    
    public int HealthRank
    {
        get => wallClass.HealthRank;
        set
        {
            wallClass.HealthRank = value;
            hpBarLength.size = new Vector2(wallClass.MaxHealth, 1);
        }
    }
    
    public bool isPlayerInRange;

    public int Health
    {
        get => wallClass.Health;
        private set
        {
            wallClass.Health = value;
            hpBar.size = new Vector2(value, 1);
            if (wallClass.Health <= 0)
            {
                StartCoroutine(GameOver());
            }
        }
    }

    private void Awake()
    {
        wallClass = LoadedData.GetSavedWall(side) ?? new WallClass();
        Ship.AddWall(this);
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;
        
        hpBar.size = new Vector2(Health, 4);
        hpBarLength.size = new Vector2(wallClass.MaxHealth, 4);
        hpBar.transform.parent.gameObject.SetActive(false);
        
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
        if (Health < wallClass.MaxHealth)
        {
            Invoke(nameof(Repair), TimeToRepair);
            Debug.Log("repairing continued");
        }
    }

    private void RepairOnSecondLife()
    {
        Health = wallClass.MaxHealth;
    }

    private void StopRepair()
    {
        hpBar.size = new Vector2(Health, 3);
        CancelInvoke(nameof(Repair));
        isRepairing = false;
    }

    private IEnumerator GameOver() //todo second life window
    {
        spriteRenderer.color = Color.black;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        if (PowerUps.SecondLife)
        {
            EventAggregator.SecondLifeActivated.Publish();
            PowerUps.ActivateSecondLife();
        }
        else
        {
            gameOver.SetActive(true);
        }
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
        if (!isPlayerInRange || Health == wallClass.MaxHealth) return;
        
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