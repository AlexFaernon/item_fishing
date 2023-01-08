using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerOnClick : MonoBehaviour
{
    [SerializeField] private Sprite normalTimeImage;
    [SerializeField] private Sprite rewindTimeImage;
    private bool isRewind;
    private Button button;
    public static float TimeScale = 1f;

    private void Awake()
    {
        button = GetComponent<Button>();
        TimeScale = 1;
    }

    private void Update()
    {
        gameObject.SetActive(!GamePhaseManager.IsBattlePhase);
        button.interactable = Time.timeScale != 0;
    }

    public void OnClickRewind()
    {
        if (isRewind)
        {
            TimeScale = Time.timeScale = 1f;
            isRewind = false;
            button.image.sprite = normalTimeImage;
        }
        else
        {
            TimeScale = Time.timeScale = 3f;
            isRewind = true;
            button.image.sprite = rewindTimeImage;
        }
    }
}
