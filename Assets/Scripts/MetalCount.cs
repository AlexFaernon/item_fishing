using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetalCount : MonoBehaviour
{
    [SerializeField] private bool isMetal;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.text = isMetal ? Resources.Metal.Count.ToString() : Resources.Electronics.Count.ToString();
    }
}
