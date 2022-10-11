using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private bool isLaunched;
    private bool isRectracting;

    private RectTransform _rectTransform;

    private SpringJoint2D spring;

    private BoxCollider2D boxCollider2D;

    // Update is called once per frame
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        spring = GetComponent<SpringJoint2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isRectracting)
        {
            if (_rectTransform.anchoredPosition.x > 110)
            {
                transform.Translate(Vector3.up * (Time.deltaTime * 3));
            }
            else
            {
                isRectracting = false;
                if (spring.connectedBody == null) return;

                Destroy(spring.connectedBody.gameObject);
                spring.connectedBody = null;
                boxCollider2D.enabled = true;
            }
            return;
        }
        
        if (isLaunched)
        {
            if (_rectTransform.anchoredPosition.x < 300)
            {
                transform.Translate(Vector3.down * (Time.deltaTime * 3));
            }
            else
            {
                isLaunched = false;
                isRectracting = true;
            }
            return;
        }

        if (!Input.GetMouseButtonDown(0)) return;
        Debug.Log("succ");
        isLaunched = true;
    }

    private void OnTriggerEnter2D(Collider2D item)
    {
        spring.connectedBody = item.attachedRigidbody;
        isLaunched = false;
        isRectracting = true;
        boxCollider2D.enabled = false;
    }
}
