using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer line;
    public GameObject hook;


    private int i;
    void Start()
    {
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hook.isLaunched)
        {
            i++;
            line.positionCount = i;

            Vector2 point = hook.GetComponent<RectTransform>().position;
            line.SetPosition(i - 1, point);
            return;
        }

        if(Hook.isRectracting)
        {

        }



    }
}
