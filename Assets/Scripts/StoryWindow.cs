using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoryWindow : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image spriteRenderer;
    [SerializeField] private Sprite woman;
    [SerializeField] private Sprite man;
    public bool IsStoryContinued { get; private set; }

    public void SetStory(bool isWoman, string story)
    {
        text.text = story;
        spriteRenderer.sprite = isWoman ? woman : man;
        IsStoryContinued = false;
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = TimerOnClick.TimeScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsStoryContinued = true;
    }
}
