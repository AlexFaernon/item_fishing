using System;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed = 5f;

    private float mx;
    private float my;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EventAggregator.ModeSwitched.Subscribe(OnModeSwitch);
    }

    private void OnModeSwitch(Mode mode)
    {
        switch (mode)
        {
            case Mode.Ship:
                rb.bodyType = RigidbodyType2D.Dynamic;
                break;
            case Mode.Player:
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (GameMode.Mode == Mode.Player) return;

        if (Hook.IsLaunched || Hook.IsRetracting)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        rb.velocity = new Vector2(mx, my).normalized * speed;
    }
}