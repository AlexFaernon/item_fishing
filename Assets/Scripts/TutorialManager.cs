using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject dialog;
    [SerializeField] private Transform level1Tutorial;
    [SerializeField] private Transform level2Tutorial;
    [SerializeField] private GameObject upgrades;
    [SerializeField] private GameObject researches;

    private void Start()
    {
        switch (LoadedData.LevelNumber)
        {
            case 1:
                StartCoroutine(Level1());
                break;
            case 2:
                StartCoroutine(Level2());
                break;
        }
    }

    private IEnumerator Level1()
    {
        yield return new WaitUntil(() => GameMode.Mode == Mode.Player);
        
        level1Tutorial.GetChild(0).gameObject.SetActive(true);
        yield return new WaitUntil(() => GameMode.Mode == Mode.Fishing);
        level1Tutorial.GetChild(0).gameObject.SetActive(false);

        level1Tutorial.GetChild(1).gameObject.SetActive(true);
        yield return new WaitUntil(() => GameMode.Mode == Mode.Player);
        level1Tutorial.GetChild(1).gameObject.SetActive(false);

        level1Tutorial.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        level1Tutorial.GetChild(2).gameObject.SetActive(false);

        level1Tutorial.GetChild(3).gameObject.SetActive(true);
        var wall = Ship.Walls.First(wall => wall.Side == Side.Up);
        yield return new WaitUntil(() => wall.Health == wall.wallClass.MaxHealth);
        level1Tutorial.GetChild(3).gameObject.SetActive(false);

        level1Tutorial.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        level1Tutorial.GetChild(4).gameObject.SetActive(false);

        level1Tutorial.GetChild(5).gameObject.SetActive(true);
        yield return new WaitUntil(() => GamePhaseManager.IsBattlePhase);
        level1Tutorial.GetChild(5).gameObject.SetActive(false);
    }

    private IEnumerator Level2()
    {
        level2Tutorial.GetChild(0).gameObject.SetActive(true);
        yield return new WaitUntil(() => upgrades.activeSelf);
        level2Tutorial.GetChild(0).gameObject.SetActive(false);

        level2Tutorial.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        level2Tutorial.GetChild(1).gameObject.SetActive(false);

        level2Tutorial.GetChild(2).gameObject.SetActive(true);
        yield return new WaitUntil(() => researches.activeSelf);
        level2Tutorial.GetChild(2).gameObject.SetActive(false);
        
        level2Tutorial.GetChild(3).gameObject.SetActive(true);
        yield return new WaitUntil(() => Research.TurretsResearch);
        level2Tutorial.GetChild(3).gameObject.SetActive(false);
        
        level2Tutorial.GetChild(4).gameObject.SetActive(true);
        yield return new WaitUntil(() => !researches.activeSelf && !upgrades.activeSelf);
        level2Tutorial.GetChild(4).gameObject.SetActive(false);

        level2Tutorial.GetChild(5).gameObject.SetActive(true);
        yield return new WaitUntil(() => Ship.Turrets.Any(turret => turret.IsInstalled));
        level2Tutorial.GetChild(5).gameObject.SetActive(false);
        
        level2Tutorial.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        level2Tutorial.GetChild(6).gameObject.SetActive(false);

    }
}
