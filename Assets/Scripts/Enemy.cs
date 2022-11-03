using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject ship;

    public int Health { get; private set; } = 5;
    public Sector CurrentSector
    {
        get
        {
            var fromTarget = (Vector2)(transform.position - ship.transform.position).normalized;
            var sign = fromTarget.y <= 0? -1.0f : 1.0f;
            var angle = Vector2.Angle(Vector2.right, fromTarget) * sign;
            return angle switch
            {
                >= 0 and < 45 => Sector.Sector0,
                >= 45 and < 90 => Sector.Sector45,
                >= 90 and < 135 => Sector.Sector90,
                >= 135 and < 180 => Sector.Sector135,
                >= -180 and < -135 => Sector.Sector180,
                >= -135 and < -90 => Sector.SectorMinus135,
                >= -90 and < -45 => Sector.SectorMinus90,
                >= -45 and < 0 => Sector.SectorMinus45,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    private bool isAttacking;
    private bool isReturning;
    private Vector2 attackVector;
    private Vector2 startPoint;

    public void Attack()
    {
        isAttacking = true;
        var position = transform.position;
        attackVector = (ship.transform.position - position).normalized;
        startPoint = position;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        EnemyAI.AddEnemy(this);
    }

    private void Update()
    {
        if (isAttacking || isReturning) return;

        transform.RotateAround(ship.transform.position, Vector3.forward, Time.deltaTime * 20);
    }

    private void FixedUpdate()
    {
        if (isAttacking || isReturning)
        {
            transform.Translate(attackVector * (Time.deltaTime * 6f), Space.World);
        }
        
        if (isReturning)
        {
            var distance = (startPoint - (Vector2)transform.position).magnitude;
            if (distance < 0.01)
            {
                transform.position = startPoint;
                isReturning = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall") && !isReturning)
        {
            isAttacking = false;
            isReturning = true;
            attackVector = -attackVector;
        }
    }
}

public enum Sector
{
    Sector0,
    Sector45,
    Sector90,
    Sector135,
    Sector180,
    SectorMinus135,
    SectorMinus90,
    SectorMinus45
}
