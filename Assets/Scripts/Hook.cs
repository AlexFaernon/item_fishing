using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private float baseX;

    public static bool isLaunched;
    public static bool isRectracting;

    private RectTransform _rectTransform;
    // Update is called once per frame
    private void Start()
    {
        baseX = transform.position.x;
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
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

        if (isRectracting)
        {
            if (_rectTransform.anchoredPosition.x > 110)
            {
                transform.Translate(Vector3.up * (Time.deltaTime * 3));
            }
            else
            {
                isRectracting = false;
            }
            return;
        }

        if (!Input.GetMouseButtonDown(0)) return;
        Debug.Log("succ");
        isLaunched = true;
    }
}
