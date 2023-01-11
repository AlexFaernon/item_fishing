using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoryWindow : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject sprite;
    public bool IsStoryContinued { get; private set; }

    public void SetStory(bool isSpriteShown, string story)
    {
        text.text = story;
        sprite.SetActive(isSpriteShown);
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
