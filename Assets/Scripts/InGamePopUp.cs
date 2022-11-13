using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePopUp : MonoBehaviour
{
    public static GameObject PointedGameObject;
    void Update()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
