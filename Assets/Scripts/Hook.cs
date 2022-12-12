using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Hook : MonoBehaviour
{
    private const float Length = 15;
    private float velocity = 10;
    
    public bool isLaunched;
    public bool isRetracting;
    
    private Vector2 initialPosition;
    
    private SpringJoint2D spring;

    private BoxCollider2D boxCollider2D;

    private AudioSource audioSource;

    private Random random = new();

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
        if (isRetracting)
        {
            var distance = ((Vector2)transform.position - initialPosition).magnitude;
            if (Math.Abs(distance) > 0.1)
            {
                transform.Translate(Vector3.down * (Time.deltaTime * velocity));
            }
            else
            {
                isRetracting = false;
                spring.attachedRigidbody.simulated = false;
                velocity = 10;
                if (spring.connectedBody == null) return;

                Destroy(spring.connectedBody.gameObject);
                switch (spring.connectedBody.tag)
                {
                    case "Metal":
                        Resources.Metal.Count += random.Next(1, 4);
                        break;
                    case "Electronics":
                        Resources.Electronics.Count += 1;
                        break;
                }
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
                transform.Translate(Vector3.up * (Time.deltaTime * 10));
            }
            else
            {
                isLaunched = false;
                isRetracting = true;
            }
            
            return;
        }
        
        if (!Input.GetMouseButtonDown(0)) return;
        audioSource.Play();
        isLaunched = true;
        spring.attachedRigidbody.simulated = true;
        initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D item)
    {
        if (!item.gameObject.CompareTag("Metal") && !item.gameObject.CompareTag("Electronics")) return;

        velocity = item.tag switch
        {
            "Metal" => Length / Resources.Metal.MaxTimeToCatch,
            "Electronics" => Length / Resources.Electronics.MaxTimeToCatch,
            _ => throw new ArgumentException()
        };
        spring.connectedBody = item.attachedRigidbody;
        item.isTrigger = true;
        isLaunched = false;
        isRetracting = true;
        boxCollider2D.enabled = false;
    }
}
