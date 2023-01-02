using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text levelNumber;
    [SerializeField] private TMP_Text turretCount;
    [SerializeField] private TMP_Text metal;
    [SerializeField] private TMP_Text electronics;
    [SerializeField] private Button startLevel;
    [SerializeField] private TMP_Text shipHealth;
    private void Awake()
    {
        EventAggregator.ShowLevelInfo.Subscribe(ShowLevelInfo);
        startLevel.onClick.AddListener(() => SceneManager.LoadScene("SampleScene")); //todo загружать монетизацию
        gameObject.SetActive(false);
    }

    private void ShowLevelInfo()
    {
        levelNumber.text = $"{LoadedData.LevelNumber} уровень";
        turretCount.text = $"{LoadedData.Turrets.Values.Count(turret => turret.IsInstalled && !turret.IsBroken)}";
        metal.text = Resources.Metal.Count.ToString();
        electronics.text = Resources.Electronics.Count.ToString();
        var wallsHealth = LoadedData.Walls.Values.Select(wall => wall.Health).Sum();
        var wallsMaxHealth = LoadedData.Walls.Values.Select(wall => wall.MaxHealth).Sum();
        shipHealth.text = $"{(int)((double)wallsHealth / wallsMaxHealth * 100)}%";
        gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventAggregator.ShowLevelInfo.Unsubscribe(ShowLevelInfo);
    }
}
