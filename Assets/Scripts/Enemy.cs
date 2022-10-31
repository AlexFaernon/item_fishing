using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject target;

    public Sector GetCurrentSector => transform.eulerAngles.z switch
    {
        >= 0 and < 45 => Sector.Sector0,
        >= 45 and < 90 => Sector.Sector45,
        >= 90 and < 135 => Sector.Sector90,
        >= 135 and < 180 => Sector.Sector135,
        >= 180 and < 225 => Sector.Sector180,
        >= 225 and < 270 => Sector.Sector225,
        >= 270 and < 315 => Sector.Sector270,
        >= 315 and < 360 => Sector.Sector315,
        _ => throw new ArgumentOutOfRangeException()
    };

    private bool isAttacking;
    private bool isReturning;
    private Vector2 attackVector;
    private Vector2 startPoint;

    private void Update()
    {
        if (isAttacking || isReturning) return;

        transform.RotateAround(target.transform.position, Vector3.forward, Time.deltaTime * 20);

        if (Input.GetKeyDown(KeyCode.G))
        {
            isAttacking = true;
            var position = transform.position;
            attackVector = (target.transform.position - position).normalized;
            Debug.Log(attackVector);
            startPoint = position;
        }
    }

    private void FixedUpdate()
    {
        if (isAttacking || isReturning)
        {
            transform.Translate(attackVector * (Time.deltaTime * 2f), Space.World);
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
        if (col.gameObject.CompareTag("Wall"))
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
    Sector225,
    Sector270,
    Sector315
}
