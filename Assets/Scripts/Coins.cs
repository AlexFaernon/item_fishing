using System;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]private TMP_Text coinsCountText;
    private YandexSDK sdk;

    private void Start()
    {
        //sdk = YandexSDK.instance;
        //sdk.onRewardedAdReward += Reward;
    }

    private void Update()
    {
        coinsCountText.text = Resources.Coins.ToString();
    }

    public void Reward(string placement)
    {
      if (placement == "coin")
      {
          PowerUps.AddToken();
      }
    }
}
