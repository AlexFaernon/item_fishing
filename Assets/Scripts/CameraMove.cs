using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = (Vector2)target.position;
    }
}
