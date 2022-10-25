using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed = 5f;
    [SerializeField] private GameObject hook;
    private Hook hookScript;

    float mx;
    float my;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hookScript = hook.GetComponent<Hook>();
    }

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (hookScript.isLaunched || hookScript.isRetracting || SwitchMode.Mode == Mode.Player)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        rb.velocity = new Vector2(mx, my).normalized * speed;
    }
}