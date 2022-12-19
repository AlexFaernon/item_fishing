using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerOnClick : MonoBehaviour
{
    [SerializeField] private Sprite normalTimeImage;
    [SerializeField] private Sprite rewindTimeImage;
    private bool isRewind;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        gameObject.SetActive(!GamePhaseManager.IsBattlePhase);
    }

    public void OnClickRewind()
    {
        if (isRewind)
        {
            Time.timeScale = 1f;
            isRewind = false;
            button.image.sprite = normalTimeImage;
        }
        else
        {
            Time.timeScale = 3f;
            isRewind = true;
            button.image.sprite = rewindTimeImage;
        }
    }
}
