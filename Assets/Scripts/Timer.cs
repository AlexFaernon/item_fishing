using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
public class Timer: MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private TMP_Text timerText;
 
    private float _timeLeft = 0f;
 
    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
        Debug.Log("Stop timer");
    }
 
    private void Start()
    {
        _timeLeft = time;
        StartCoroutine(StartTimer());
        Time.timeScale = 3f; // перемотка
    }
 
    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;
 
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}