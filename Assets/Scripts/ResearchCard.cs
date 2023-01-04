using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResearchCard : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject description;
    [SerializeField] private Button button;
    [SerializeField] private ResearchType researchType;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private int cost;
    private TMP_Text descriptionText;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
        descriptionText = description.GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = TimerOnClick.TimeScale;
    }

    private void OnClick()
    {
        switch (researchType)
        {
            case ResearchType.Turrets:
                Research.TurretsResearch = true;
                break;
            case ResearchType.TwoTurrets:
                Research.TwoTurretsResearch = true;
                break;
            case ResearchType.Barrier:
                Research.BarriersResearch = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        button.gameObject.SetActive(false);
        description.SetActive(true);
        Resources.Electronics.Count -= cost;
    }

    private void Update()
    {
        costText.text = $"{Resources.Electronics.Count}/{cost}";
        if (Resources.Electronics.Count < cost)
        {
            costText.color = Color.red;
            button.interactable = false;
            return;
        }
        
        costText.color = Color.black;
        switch (researchType)
        {
            case ResearchType.Turrets:
                button.interactable = !Research.TurretsResearch;
                break;
            case ResearchType.TwoTurrets:
                button.interactable = !Research.TwoTurretsResearch;
                break;
            case ResearchType.Barrier:
                button.interactable = !Research.BarriersResearch;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        descriptionText.color = Color.black;
        switch (researchType)
        {
            case ResearchType.Turrets:
                if (Research.TurretsResearch)
                {
                    descriptionText.color = Color.red;
                }
                break;
            case ResearchType.TwoTurrets:
                if (!Research.TurretsResearch || Research.TwoTurretsResearch)
                {
                    descriptionText.color = Color.red;
                }
                break;
            case ResearchType.Barrier:
                if (!Research.TwoTurretsResearch || Research.BarriersResearch)
                {
                    descriptionText.color = Color.red;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (researchType)
        {
            case ResearchType.Turrets:
                if (Research.TurretsResearch)
                    return;
                break;
            case ResearchType.TwoTurrets:
                if (!Research.TurretsResearch || Research.TwoTurretsResearch)
                    return;
                break;
            case ResearchType.Barrier:
                if (!Research.TwoTurretsResearch || Research.BarriersResearch)
                    return;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        button.gameObject.SetActive(!button.gameObject.activeSelf);
        description.SetActive(!description.activeSelf);
    }
}

public enum ResearchType
{
    Turrets,
    TwoTurrets,
    Barrier
}
