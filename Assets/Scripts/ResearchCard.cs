using System;
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
    [SerializeField] private GameObject electronicsIcon;
    [SerializeField] private GameObject fade;
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
        }
        else
        {
            costText.color = Color.black;
            button.interactable = true;
        }

        switch (researchType)
        {
            case ResearchType.Turrets:
                descriptionText.color = Research.TurretsResearch ? Color.green : Color.black;
                costText.gameObject.SetActive(!Research.TurretsResearch);
                electronicsIcon.SetActive(!Research.TurretsResearch);
                break;
            case ResearchType.TwoTurrets:
                fade.SetActive(!Research.TurretsResearch);
                costText.gameObject.SetActive(!Research.TwoTurretsResearch);
                electronicsIcon.SetActive(!Research.TwoTurretsResearch);
                descriptionText.color = Research.TwoTurretsResearch ? Color.green : Color.black;
                break;
            case ResearchType.Barrier:
                fade.SetActive(!Research.TwoTurretsResearch);
                costText.gameObject.SetActive(!Research.BarriersResearch);
                electronicsIcon.SetActive(!Research.BarriersResearch);
                descriptionText.color = Research.BarriersResearch ? Color.green : Color.black;
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
