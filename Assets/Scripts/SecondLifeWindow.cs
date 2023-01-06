using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLifeWindow : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(OnSecondLife());
    }

    private IEnumerator OnSecondLife()
    {
        Time.timeScale = 0;
        PowerUps.UseSecondLife();
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1;
        EventAggregator.SecondLifeActivated.Publish();
        gameObject.SetActive(false);
    }
}
