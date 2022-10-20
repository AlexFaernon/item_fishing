using System;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private const float Length = 3;
    
    public bool isLaunched;
    public bool isRetracting;
    
    private Vector2 initialPosition;
    
    private SpringJoint2D spring;

    private BoxCollider2D boxCollider2D;

    private AudioSource audioSource;

    // Update is called once per frame
    private void Start()
    {
        spring = GetComponent<SpringJoint2D>();
        spring.attachedRigidbody.simulated = false;
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isLaunched || isRetracting) return;
        
        if (!Input.GetMouseButtonDown(0)) return;
        audioSource.Play();
        isLaunched = true;
        spring.attachedRigidbody.simulated = true;
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isRetracting)
        {
            var distance = ((Vector2)transform.position - initialPosition).magnitude;
            if (Math.Abs(distance) > 0.01)
            {
                transform.Translate(Vector3.down * (Time.deltaTime * 3));
            }
            else
            {
                isRetracting = false;
                spring.attachedRigidbody.simulated = false;
                if (spring.connectedBody == null) return;

                Destroy(spring.connectedBody.gameObject);
                spring.connectedBody = null;
                boxCollider2D.enabled = true;
            }
            return;
        }
        
        if (isLaunched)
        {
            var distance = ((Vector2)transform.position - initialPosition).magnitude;
            if (distance < Length)
            {
                transform.Translate(Vector3.up * (Time.deltaTime * 3));
            }
            else
            {
                isLaunched = false;
                isRetracting = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D item)
    {
        spring.connectedBody = item.attachedRigidbody;
        isLaunched = false;
        isRetracting = true;
        boxCollider2D.enabled = false;
    }
}
