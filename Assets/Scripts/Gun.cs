using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject hook;
    private LineRenderer line;
    private Hook hookScript;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        hookScript = hook.GetComponent<Hook>();
    }

    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.transform.position);
        if (hookScript.isLaunched || hookScript.isRetracting) return;
        TurnToMouse();
    }

    private void TurnToMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);//немного магии на последок
    }
}
