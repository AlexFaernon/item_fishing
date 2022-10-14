using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed = 5f;

    float mx;
    float my;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}