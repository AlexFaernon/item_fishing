using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private GameObject player;

    private float mx;
    private float my;

    private void Awake()
    {
        EventAggregator.ModeSwitched.Subscribe(SetActiveOnMod);
        //gameObject.SetActive(false);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        if (rb.velocity != Vector2.zero)
        {
            player.transform.right = rb.velocity;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx, my).normalized * speed;
    }
    
    private void SetActiveOnMod(Mode mode)
    {
        switch (mode)
        {
            case Mode.Fishing:
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
        EventAggregator.ModeSwitched.Unsubscribe(SetActiveOnMod);
    }
}
