using UnityEngine;

public class BackgroundParalax : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ship;
    [SerializeField] private float multiplayer;
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (ship.bodyType == RigidbodyType2D.Static)
        {
            rigidbody2D.velocity = Vector2.zero;
            return;
        }
        
        rigidbody2D.velocity = ship.velocity * -multiplayer;
    }
}
