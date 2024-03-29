using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text turretCount;
    [SerializeField] private TMP_Text metal;
    [SerializeField] private TMP_Text electronics;
    [SerializeField] private TMP_Text shipHealth;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        Time.timeScale = 0;
        var shipImage = shipHealth.transform.parent.GetComponent<Image>();
        turretCount.text = $"{Ship.Turrets.Count(turret => turret.IsInstalled && !turret.IsBroken)}";
        metal.text = Resources.Metal.Count.ToString();
        electronics.text = Resources.Electronics.Count.ToString();
        var wallsHealth = Ship.Walls.Select(wall => wall.Health).Sum();
        var wallsMaxHealth = Ship.Walls.Select(wall => wall.wallClass.MaxHealth).Sum();

        var healthPercent = (double)wallsHealth / wallsMaxHealth * 100;
        shipHealth.text = $"{(int)healthPercent}%";
        shipImage.color = healthPercent switch
        {
            >= 66 => Color.green,
            < 66 and >= 33 => Color.yellow,
            < 33 => Color.red,
            _ => throw new ArgumentException()
        };

        continueButton.onClick.AddListener(Continue);
        replayButton.onClick.AddListener(Replay);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void Continue()
    {
        SceneManager.LoadScene("Levels");
    }

    private void Replay()
    {
        Level.ReloadLevel();
        SceneManager.LoadScene("SampleScene");
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
