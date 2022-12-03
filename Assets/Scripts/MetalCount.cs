using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetalCount : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        EventAggregator.MetalUpdate.Subscribe(UpdateText);
        UpdateText(Resources.Metal.Count);
    }

    private void UpdateText(int metalCount)
    {
        text.text = "МИТАЛ " + metalCount;
    }

    private void OnDestroy()
    {
        EventAggregator.MetalUpdate.Unsubscribe(UpdateText);
    }
}
