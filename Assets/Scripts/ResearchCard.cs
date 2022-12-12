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

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
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
    }

    private enum ResearchType
    {
        Turrets,
        TwoTurrets,
        Barrier
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        button.gameObject.SetActive(!button.gameObject.activeSelf);
        description.SetActive(!description.activeSelf);
    }
}
