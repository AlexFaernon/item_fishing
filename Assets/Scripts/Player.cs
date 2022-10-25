using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed = 0.5f;

    float mx;
    float my;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EventAggregator.ModeSwitched.Subscribe(SetActiveOnMod);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx, my).normalized * speed;
    }
    
    private void SetActiveOnMod(Mode mode)
    {
        switch (mode)
        {
            case Mode.Ship:
                gameObject.SetActive(false);
                break;
            case Mode.Player:
                gameObject.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    private void OnDestroy()
    {
        EventAggregator.ModeSwitched.Subscribe(SetActiveOnMod);
    }
}
