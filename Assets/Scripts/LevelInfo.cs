using System;
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
    [SerializeField] private TMP_Text time;
    private Image shipImage;
    private void Awake()
    {
        shipImage = shipHealth.transform.parent.GetComponent<Image>();
        EventAggregator.ShowLevelInfo.Subscribe(ShowLevelInfo);
        startLevel.onClick.AddListener(StartLevel);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    private void ShowLevelInfo()
    {
        levelNumber.text = $"{LoadedData.LevelNumber} уровень";
        
        float preparationTime = LoadedData.PreparationTime;
        float battleTime = LoadedData.BattleTime;
        time.text =
            $"{Mathf.FloorToInt(preparationTime / 60):00}:{Mathf.FloorToInt(preparationTime % 60):00} + {Mathf.FloorToInt(battleTime / 60):00}:{Mathf.FloorToInt(battleTime % 60):00}";
        
        turretCount.text = $"{LoadedData.Turrets.Values.Count(turret => turret.IsInstalled && !turret.IsBroken)}";
        metal.text = Resources.Metal.Count.ToString();
        electronics.text = Resources.Electronics.Count.ToString();
        
        if (LoadedData.LevelNumber == 1)
        {
            shipHealth.text = "???";
            shipImage.color = Color.gray;
        }
        else
        {
            var wallsHealth = LoadedData.Walls.Values.Select(wall => wall.Health).Sum();
            var wallsMaxHealth = LoadedData.Walls.Values.Select(wall => wall.MaxHealth).Sum();
            
            var healthPercent = (double)wallsHealth / wallsMaxHealth * 100;
            shipHealth.text = $"{(int)healthPercent}%";
            shipImage.color = healthPercent switch
            {
                >= 66 => Color.green,
                < 66 and >= 33 => Color.yellow,
                < 33 => Color.red,
                _ => throw new ArgumentException()
            };
        }
        gameObject.SetActive(true);
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(LoadedData.LevelNumber == 1 ? "SampleScene" : "Monetization");
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
