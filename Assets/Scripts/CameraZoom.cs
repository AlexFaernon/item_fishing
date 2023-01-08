using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    private float currentZoom;
    private float speedZoom = 10;
    private float valueZoom = 3f;

    private void Start()
    {
        cam = Camera.main;
        if (cam != null) currentZoom = cam.orthographicSize;
    }

    private void Update()
    {
        float scroll;
        scroll = Input.GetAxis("Mouse ScrollWheel");
        
        currentZoom -= scroll * valueZoom;
        currentZoom = Mathf.Clamp(currentZoom, 10f, 22f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, currentZoom, Time.deltaTime * speedZoom);
    }
}
