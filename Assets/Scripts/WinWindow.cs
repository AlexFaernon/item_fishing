using System.Collections;
using System.Collections.Generic;
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
        turretCount.text = $"{Ship.Turrets.Count(turret => turret.IsInstalled && !turret.IsBroken)}";
        metal.text = Resources.Metal.Count.ToString();
        electronics.text = Resources.Electronics.Count.ToString();
        var wallsHealth = Ship.Walls.Select(wall => wall.Health).Sum();
        var wallsMaxHealth = Ship.Walls.Select(wall => wall.wallClass.MaxHealth).Sum();
        shipHealth.text = $"{(int)((double)wallsHealth / wallsMaxHealth * 100)}%";
        continueButton.onClick.AddListener(Continue);
    }

    private void Continue()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Levels");
    }
}
