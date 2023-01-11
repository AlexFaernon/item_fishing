using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyTurretBoost : MonoBehaviour
{
    private Button button;
    private TMP_Text text;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        text = transform.GetChild(0).GetComponent<TMP_Text>();
        button.onClick.AddListener(Buy);
    }

    private void Update()
    {
        if (PowerUps.TurretsBoost)
        {
            text.text = "Куплено";
        }

        button.interactable = !PowerUps.TurretsBoost && Resources.Coins >= 1;
    }

    private void Buy()
    {
        PowerUps.BuyTurretBoost();
    }
}
